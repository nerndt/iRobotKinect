using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using CRPixel.MaNet;

// 
// Savitzky-Golay filter implementation. For more information see
// http://www.nrbook.com/a/bookcpdf/c14-8.pdf. This implementation,
// however, does not use FFT
// 

namespace CRPixel
{
    // Example of how to use the filter below
    // float[] data = new float[] { 8916.81f, 8934.24f, 9027.06f, 9160.79f, 7509.14f };
    // float[] leftPad = new float[] { 8915.06f, 8845.53f, 9064.17f, 8942.09f, 8780.87f };
    // double[] coeffs = SGFilter.computeSGCoefficients(5, 5, 4);
    // ContinuousPadder padder1 = new ContinuousPadder();
    // SGFilter sgFilter = new SGFilter(5, 5);
    // sgFilter.appendPreprocessor(padder1);
    // float[] smooth = sgFilter.smooth(data, leftPad, new float[0], coeffs);
    // MeanValuePadder padder2 = new MeanValuePadder(10, false, true);
    // sgFilter.removePreprocessor(padder1);
    // sgFilter.appendPreprocessor(padder2);
    // smooth = sgFilter.smooth(data, leftPad, new float[0], coeffs);
    // Example of how to use the filter above

    // double[] coeffs = SGFilter.computeSGCoefficients(5, 5, 4);
    // SGFilter sgFilter = new SGFilter(5, 5);
    // In my case use // float[] smooth = sgFilter.smooth(data, new float[0], new float[0], coeffs); // No left of right padding
    
    // 
    // This interface represents types which are able to filter data, for example:
    // eliminate redundant points.
    // 
    // see SGFilter#appendPreprocessor(Preprocessor)
    // 
    public interface DataFilter
    {
        double[] filter(double[] data);
    }

    // 
    // This interface represents types which are able to perform data processing in
    // place. Useful examples include: eliminating zeros, padding etc.
    // 
    // see SGFilter#appendPreprocessor(Preprocessor)
    // 
    public interface Preprocessor
    {
        // 
        // Data processing method. Called on Preprocessor instance when its
        // processing is needed
        // 
        // input data
        // 
        void apply(double[] data);
    }

    public class SGFilter
    {

        // 
        // Constructs Savitzky-Golay filter which uses specified numebr of
        // surrounding data points
        // 
        // input nl
        //            numer of past data points filter will use
        // input nr
        //            numer of future data points filter will use
        // throws System.ArgumentException
        //             of {nl < 0} or {nr < 0}
        // 
        public SGFilter(int nl, int nr)
        {
            if (nl < 0 || nr < 0)
                throw new System.ArgumentException("Bad arguments");
            this.nl = nl;
            this.nr = nr;
        }

        // 
        // Computes Savitzky-Golay coefficients for given parameters
        // 
        // input nl
        //            numer of past data points filter will use
        // input nr
        //            number of future data points filter will use
        // input degree
        //            order of smoothin polynomial
        // return Savitzky-Golay coefficients
        // throws System.ArgumentException
        //             if {nl < 0} or {nr < 0} or {nl + nr <
        //             degree}
        // 
        public static double[] computeSGCoefficients(int nl, int nr, int degree, int orderOfDervative = 0)
        {
            if (nl < 0 || nr < 0 || nl + nr < degree)
                throw new System.ArgumentException("Bad arguments");
            Matrix matrix = new Matrix(degree + 1, degree + 1);
            double[][] a = matrix.Array;
            double sum;
            for (int i = 0; i <= degree; i++)
            {
                for (int j = 0; j <= degree; j++)
                {
                    sum = (i == 0 && j == 0) ? 1 : 0;
                    for (int k = 1; k <= nr; k++)
                        sum += Math.Pow(k, i + j);
                    for (int k = 1; k <= nl; k++)
                        sum += Math.Pow(-k, i + j);
                    a[i][j] = sum;
                }
            }
            double[] b = new double[degree + 1];
            b[orderOfDervative] = 1;

            // My code to perform b = matrix.Solve(b);
            Matrix A = new Matrix(b);
            Matrix x = matrix.Solve(A);
            b = x.GetColumnArray(0);

            double[] coeffs = new double[nl + nr + 1];
            for (int n = -nl; n <= nr; n++)
            {
                sum = b[0];
                for (int m = 1; m <= degree; m++)
                    sum += b[m] * Math.Pow(n, m);
                coeffs[n + nl] = sum;
            }
            return coeffs;
        }

        public static double[] computeSGCoefficients(int nPoints, int degreePolynomial)
        {
            int np = nPoints;
            int sf = degreePolynomial;
            int mp = (np - 1) / 2;

            int i, j, k;
            double[][] S = new double[np][];
            for (i = 0; i < np; i++)
            {
                S[i] = new double[sf + 1];
                for (j = 0; j <= sf; j++)
                {
                    S[i][j] = Math.Pow(i - mp, j);
                }
            }

            double[][] F = new double[sf + 1][];
            for (i = 0; i <= sf; i++)
            {
                F[i] = new double[sf + 1];
                for (j = 0; j <= sf; j++)
                {
                    F[i][j] = 0.0;
                    for (k = 0; k < np; k++)
                    {
                        F[i][j] += S[k][i] * S[k][j];
                    }
                }
            }

            double[][] G = new double[np][];               // will contain derivative filters
            for (i = 0; i < np; i++)
            {
                G[i] = new double[sf + 1];
                for (j = 0; j <= sf; j++)
                {
                    G[i][j] = 0.0;
                }
            }

            double[][] B = new double[np][];                // will contain smoothing filters
            for (i = 0; i < np; i++)
            {
                B[i] = new double[np];
                for (j = 0; j < np; j++)
                {
                    B[i][j] = 0.0;
                }
            }


            invert(F);

            for (j = 0; j <= sf; j++)
            {
                for (i = 0; i < np; i++)
                {
                    for (k = 0; k <= sf; k++)
                    {
                        G[i][j] += S[i][k] * F[k][j];
                    }
                }
            }

            for (j = 0; j < np; j++)
            {
                for (i = 0; i < np; i++)
                {
                    for (k = 0; k <= sf; k++)
                    {
                        B[i][j] += G[i][k] * S[j][k];
                    }
                }
            }

            return B[np/2];
        }

        public static void invert(double[][] Mt)
{
    int i = 0;
    int j = 0;
    int k = 0;
    var m = 0;
    double Amax = 0;
    double pivot = 0;
    double dummy = 0;
    double xmult = 0;
    double ymult = 0;

    var n = Mt.Length;

    double[][] Mx = new double[n][];
    double[][] My = new double[n][];
    for (i = 0; i < n; i++)
    {          // create identity matrices

        Mx[i] = new double[n];
        My[i] = new double[n];
        for (j = 0; j < n; j++)
        {

            Mx[i][j] = 0.0;
            My[i][j] = 0.0;
        }
        Mx[i][i] = 1.0;
        My[i][i] = 1.0;
    }

    int irow, icol;

    for (i = 0; i < n; i++)
    {

        Amax = Mt[i][i];
        irow = i;
        icol = i;

        for (k = i; k < n; k++)
        {          // find element with largest
            for (m = i; m < n; m++)
            {        // value
                if (Math.Abs(Amax) < Math.Abs(Mt[k][m]))
                {

                    irow = k;
                    icol = m;
                    Amax = Mt[k][m];
                }
            }
        }

        for (k = 0; k < n; k++)
        {          // interchange rows

            dummy = Mx[i][k];
            Mx[i][k] = Mx[irow][k];
            Mx[irow][k] = dummy;

            dummy = Mt[i][k];
            Mt[i][k] = Mt[irow][k];
            Mt[irow][k] = dummy;
        }

        for (k = 0; k < n; k++)
        {         // interchange columns

            dummy = My[k][i];
            My[k][i] = My[k][icol];
            My[k][icol] = dummy;

            dummy = Mt[k][i];
            Mt[k][i] = Mt[k][icol];
            Mt[k][icol] = dummy;
        }

        pivot = Mt[i][i];

        for (k = 0; k < n; k++)
        {
            Mt[k][i] /= pivot;
            My[k][i] /= pivot;
        }

        int ip1;
        if (i != n - 1)
        {

            ip1 = i + 1;

            for (m = ip1; m < n; m++)
            {

                ymult = Mt[i][m];

                for (k = 0; k < n; k++)
                {

                    Mt[k][m] -= Mt[k][i] * ymult;
                    My[k][m] -= My[k][i] * ymult;
                }
            }

            for (m = ip1; m < n; m++)
            {

                xmult = Mt[m][i];

                for (k = 0; k < n; k++)
                {

                    Mt[m][k] -= Mt[i][k] * xmult;
                    Mx[m][k] -= Mx[i][k] * xmult;
                }

            }
        }
    }

    for (k = 0; k < n; k++)
    {
        for (m = 0; m < n; m++)
        {

            Mt[k][m] = 0.0;

            for (j = 0; j < n; j++)
            {

                Mt[k][m] += My[k][j] * Mx[j][m];
            }
        }
    }
}


        private static void convertDoubleArrayToFloat(double[] input, float[] output)
        {
            for (int i = 0; i < input.Length; i++)
                output[i] = (float)input[i];
        }

        private static void convertFloatArrayToDouble(float[] input, double[] output)
        {
            for (int i = 0; i < input.Length; i++)
                output[i] = input[i];
        }

        private List<DataFilter> dataFilters = new List<DataFilter>(); // Constant new ArrayList<DataFilter>(); // Constant

        private int nl;

        private int nr;

        private List<Preprocessor> preprocessors = new List<Preprocessor>(); // Constant new ArrayList<Preprocessor>();

        // 
        // Appends data filter
        // 
        // input dataFilter
        //            dataFilter
        // see DataFilter
        // 
        public void appendDataFilter(DataFilter dataFilter)
        {
            dataFilters.Add(dataFilter);
        }

        // 
        // Appends data preprocessor
        // 
        // input p
        //            preprocessor
        // see Preprocessor
        // 
        public void appendPreprocessor(Preprocessor p)
        {
            preprocessors.Add(p);
        }

        // 
        // 
        // return number of past data points that this filter uses
        // 
        public int getNl()
        {
            return nl;
        }

        // 
        // 
        // return number of future data points that this filter uses
        // 
        int getNr()
        {
            return nr;
        }

        // 
        // Inserts data filter
        // 
        // input dataFilter
        //            data filter
        // input index
        //            where it should be placed in data filters queue
        // see DataFilter
        // 
        public void insertDataFilter(DataFilter dataFilter, int index)
        {
            dataFilters.Insert(index, dataFilter);
        }

        // 
        // Inserts preprocessor
        // 
        // param p
        //           preprocessor
        // param index
        //           where it should be placed in preprocessors queue
        // see Preprocessor
        // 
        public void insertPreprocessor(Preprocessor p, int index)
        {
            preprocessors.Insert(index, p);
        }

        // 
        // Removes data filter
        // 
        // input dataFilter
        //            data filter to be removed
        // return {true} if data filter existed and was removed, {false} otherwise
        // 
        public bool removeDataFilter(DataFilter dataFilter)
        {
            return dataFilters.Remove(dataFilter);
        }

        // 
        // Removes data filter
        // 
        // input index
        //            which data filter to remove
        // return removed data filter
        // 
        public void removeDataFilter(int index)
        {
            dataFilters.RemoveAt(index);
        }

        // 
        // Removes preprocessor
        // 
        // input index
        //            which preprocessor to remove
        // return removed preprocessor
        // 
        public void removePreprocessor(int index)
        {
            preprocessors.RemoveAt(index);
        }

        // 
        // Removes preprocessor
        // 
        // input p
        //            preprocessor to be removed
        // return {true} if preprocessor existed and was removed, {false} otherwise
        // 
        public bool removePreprocessor(Preprocessor p)
        {
            return preprocessors.Remove(p);
        }

        // 
        // Sets number of past data points for this filter
        // 
        // input nl
        //            number of past data points
        // throws ArgumentException if {nl < 0}
        // 
        public void setNl(int nl)
        {
            if (nl < 0)
                throw new System.ArgumentException("nl < 0");
            this.nl = nl;
        }

        // 
        // Sets number of future data points for this filter
        // 
        // input nr
        //            number of future data points
        // throws ArgumentException if {nr < 0}
        // 
        public void setNr(int nr)
        {
            if (nr < 0)
                throw new System.ArgumentException("nr < 0");
            this.nr = nr;
        }

        // 
        // Smooths data by using Savitzky-Golay filter. This method will use 0 for
        // any element beyond {data} which will be needed for computation (you
        // may want to use some {Preprocessor})
        // 
        // input data
        //            data for filter
        // input coeffs
        //            filter coefficients
        // return filtered data
        // throws NullPointerException
        //             when any array passed as parameter is null
        // 
        public double[] smooth(double[] data, double[] coeffs)
        {
            return smooth(data, 0, data.Length, coeffs);
        }

        // 
        // Smooths data by using Savitzky-Golay filter. Smoothing uses {leftPad} and/or {rightPad} if you want to augment data on
        // boundaries to achieve smoother results for your purpose. If you do not
        // need this feature you may pass empty arrays (filter will use 0s in this
        // place, so you may want to use appropriate preprocessor)
        // 
        // input data
        //            data for filter
        // input leftPad
        //            left padding
        // input rightPad
        //            right padding
        // input coeffs
        //            filter coefficients
        // return filtered data
        // throws NullPointerException
        //             when any array passed as parameter is null
        // 
        public double[] smooth(double[] data, double[] leftPad, double[] rightPad,
                double[] coeffs)
        {
            return smooth(data, leftPad, rightPad, 0, new double[][] { coeffs });
        }

        // 
        // Smooths data by using Savitzky-Golay filter. Smoothing uses {leftPad} and/or {rightPad} if you want to augment data on
        // boundaries to achieve smoother results for your purpose. If you do not
        // need this feature you may pass empty arrays (filter will use 0s in this
        // place, so you may want to use appropriate preprocessor). If you want to
        // use different (probably non-symmetrical) filter near both ends of
        // (padded) data, you will be using {bias} and {coeffs}. {bias} essentially means
        // "how many points of pad should be left out when smoothing". Filters
        // taking this condition into consideration are passed in {coeffs}.
        // <tt>coeffs[0]</tt> is used for unbiased data (that is, for
        // <tt>data[bias]..data[data.Length-bias-1]</tt>). Its length has to be
        // <tt>nr + nl + 1</tt>. Filters from range
        // <tt>coeffs[coeffs.Length - 1]</tt> to
        // <tt>coeffs[coeffs.Length - bias]</tt> are used for smoothing first
        // {bias} points (that is, from <tt>data[0]</tt> to
        // <tt>data[bias]</tt>) correspondingly. Filters from range
        // <tt>coeffs[1]</tt> to <tt>coeffs[bias]</tt> are used for smoothing last
        // {bias} points (that is, for
        // <tt>data[data.Length-bias]..data[data.Length-1]</tt>). For example, if
        // you use 5 past points and 5 future points for smoothing, but have only 3
        // meaningful padding points - you would use {bias} equal to 2 and
        // would pass in {coeffs} param filters taking 5-5 points (for regular
        // smoothing), 5-4, 5-3 (for rightmost range of data) and 3-5, 4-5 (for
        // leftmost range). If you do not wish to use pads completely for
        // symmetrical filter then you should pass <tt>bias = nl = nr</tt>
        // 
        // input data
        //            data for filter
        // input leftPad
        //            left padding
        // input rightPad
        //            right padding
        // input bias
        //            how many points of pad should be left out when smoothing
        // input coeffs
        //            array of filter coefficients
        // return filtered data
        // throws System.ArgumentException
        //             when <tt>bias < 0</tt> or <tt>bias > min(nr, nl)</tt>
        // throws IndexOutOfBoundsException
        //             when {coeffs} has less than <tt>2*bias + 1</tt>
        //             elements
        // throws NullPointerException
        //             when any array passed as parameter is null
        // 
        public double[] smooth(double[] data, double[] leftPad, double[] rightPad,
                int bias, double[][] coeffs)
        {
            if (bias < 0 || bias > nr || bias > nl)
                throw new System.ArgumentException(
                        "bias < 0 or bias > nr or bias > nl");
            foreach (DataFilter dataFilter in dataFilters)
            {    // for (ArrayList dataFilter : dataFilters) {
                data = dataFilter.filter(data);                 // data = dataFilter.filter(data);
            }                                                   // }
            int dataLength = data.Length;
            if (dataLength == 0)
                return data;
            int n = dataLength + nl + nr;
            double[] dataCopy = new double[n];
            // copy left pad reversed
            int leftPadOffset = nl - leftPad.Length;
            if (leftPadOffset >= 0)
                for (int i = 0; i < leftPad.Length; i++)
                {
                    dataCopy[leftPadOffset + i] = leftPad[i];
                }
            else
                for (int i = 0; i < nl; i++)
                {
                    dataCopy[i] = leftPad[i - leftPadOffset];
                }
            // copy actual data
            for (int i = 0; i < dataLength; i++)
            {
                dataCopy[i + nl] = data[i];
            }
            // copy right pad
            int rightPadOffset = nr - rightPad.Length;
            if (rightPadOffset >= 0)
                for (int i = 0; i < rightPad.Length; i++)
                {
                    dataCopy[i + dataLength + nl] = rightPad[i];
                }
            else
                for (int i = 0; i < nr; i++)
                {
                    dataCopy[i + dataLength + nl] = rightPad[i];
                }
            foreach (Preprocessor p in preprocessors)
            {
                p.apply(dataCopy);
            }

            // convolution (with savitzky-golay coefficients)
            double[] sdata = new double[dataLength];
            double[] sg;
            for (int b = bias; b > 0; b--)
            {
                sg = coeffs[coeffs.Length - b];
                int x = (nl + bias) - b;
                double sum = 0;
                for (int i = -nl + b; i <= nr; i++)
                {
                    sum += dataCopy[x + i] * sg[nl - b + i];
                }
                sdata[x - nl] = sum;
            }
            sg = coeffs[0];
            for (int x = nl + bias; x < n - nr - bias; x++)
            {
                double sum = 0;
                for (int i = -nl; i <= nr; i++)
                {
                    sum += dataCopy[x + i] * sg[nl + i];
                }
                sdata[x - nl] = sum;
            }
            for (int b = 1; b <= bias; b++)
            {
                sg = coeffs[b];
                int x = (n - nr - bias) + (b - 1);
                double sum = 0;
                for (int i = -nl; i <= nr - b; i++)
                {
                    sum += dataCopy[x + i] * sg[nl + i];
                }
                sdata[x - nl] = sum;
            }
            return sdata;
        }

        // 
        // Runs filter on data from {from} (including) to {to}
        // (excluding). Data beyond range spanned by {from} and {to}
        // will be used for padding
        // 
        // input data
        //            data for filter
        // input from
        //            inedx of the first element of data
        // input to
        //            index of the first element omitted
        // input coeffs
        //            filter coefficients
        // return filtered data
        // throws ArrayIndexOutOfBoundsException
        //             if <tt>to > data.Length</tt>
        // throws System.ArgumentException
        //             if <tt>from < 0</tt> or <tt>to > data.Length</tt>
        // throws NullPointerException
        //             if {data} is null or {coeffs} is null
        // 
        public double[] smooth(double[] data, int from, int to, double[] coeffs)
        {
            return smooth(data, from, to, 0, new double[][] { coeffs });
        }

        // 
        // Runs filter on data from {from} (including) to {to}
        // (excluding). Data beyond range spanned by {from} and {to}
        // will be used for padding. See
        // {#smooth(double[], double[], double[], int, double[][])} for usage
        // of {bias}
        // 
        // input data
        //            data for filter
        // input from
        //            inedx of the first element of data
        // input to
        //            index of the first element omitted
        // input bias
        //            how many points of pad should be left out when smoothing
        // input coeffs
        //            filter coefficients
        // return filtered data
        // throws ArrayIndexOutOfBoundsException
        //             if <tt>to > data.Length</tt> or when {coeffs} has less
        //             than <tt>2*bias + 1</tt> elements
        // throws System.ArgumentException
        //             if <tt>from < 0</tt> or <tt>to > data.Length</tt> or
        //             <tt>from > to</tt> or when <tt>bias < 0</tt> or
        //             <tt>bias > min(nr, nl)</tt>
        // throws NullPointerException
        //             if {data} is null or {coeffs} is null
        // 
        public double[] smooth(double[] data, int from, int to, int bias, double[][] coeffs)
        {
            //double[] leftPad = Arrays.copyOfRange(data, 0, from);
            double[] leftPad = new double[from];
            Array.Copy(leftPad, 0, data, 0, from);
            //double[] rightPad = Arrays.copyOfRange(data, to, data.Length);
            double[] rightPad = new double[data.Length - to];
            Array.Copy(rightPad, 0, data, to, data.Length);
            //double[] dataCopy = Arrays.copyOfRange(data, from, to);
            double[] dataCopy = new double[to - from];
            Array.Copy(dataCopy, 0, data, from, to);
            return smooth(dataCopy, leftPad, rightPad, bias, coeffs);
        }

        // 
        // See {#smooth(double[], double[])}. This method converts {data} to double for computation and then converts it back to float
        // 
        // input data
        //            data for filter
        // input coeffs
        //            filter coefficients
        // return filtered data
        // throws NullPointerException
        //             when any array passed as parameter is null
        // 
        public float[] smooth(float[] data, double[] coeffs)
        {
            return smooth(data, 0, data.Length, coeffs);
        }

        // 
        // See {#smooth(double[], double[], double[], double[])}. This method
        // converts {data} {leftPad} and {rightPad} to double for
        // computation and then converts back to float
        // 
        // input data
        //            data for filter
        // input leftPad
        //            left padding
        // input rightPad
        //            right padding
        // input coeffs
        //            filter coefficients
        // return filtered data
        // throws NullPointerException
        //             when any array passed as parameter is null
        // 
        public float[] smooth(float[] data, float[] leftPad, float[] rightPad,
                double[] coeffs)
        {
            return smooth(data, leftPad, rightPad, 0, new double[][] { coeffs });
        }

        // 
        // See {#smooth(double[], double[], double[], int, double[][])}. This
        // method converts {data} {leftPad} and {rightPad} to
        // double for computation and then converts back to float
        // 
        // input data
        //            data for filter
        // input leftPad
        //            left padding
        // input rightPad
        //            right padding
        // input bias
        //            how many points of pad should be left out when smoothing
        // input coeffs
        //            array of filter coefficients
        // return filtered data
        // throws System.ArgumentException
        //             when <tt>bias < 0</tt> or <tt>bias > min(nr, nl)</tt>
        // throws IndexOutOfBoundsException
        //             when {coeffs} has less than <tt>2*bias + 1</tt>
        //             elements
        // throws NullPointerException
        //             when any array passed as parameter is null
        // 
        public float[] smooth(float[] data, float[] leftPad, float[] rightPad,
                int bias, double[][] coeffs)
        {
            double[] dataAsDouble = new double[data.Length];
            double[] leftPadAsDouble = new double[leftPad.Length];
            double[] rightPadAsDouble = new double[rightPad.Length];
            convertFloatArrayToDouble(data, dataAsDouble);
            convertFloatArrayToDouble(leftPad, leftPadAsDouble);
            convertFloatArrayToDouble(rightPad, rightPadAsDouble);
            double[] results = smooth(dataAsDouble, leftPadAsDouble,
                    rightPadAsDouble, bias, coeffs);
            float[] resultsAsFloat = new float[results.Length];
            convertDoubleArrayToFloat(results, resultsAsFloat);
            return resultsAsFloat;
        }

        // 
        // See {#smooth(double[], int, int, double[])}. This method converts
        // {data} to double for computation and then converts it back to float
        // 
        // input data
        //            data for filter
        // input from
        //            inedx of the first element of data
        // input to
        //            index of the first element omitted
        // input coeffs
        //            filter coefficients
        // return filtered data
        // throws ArrayIndexOutOfBoundsException
        //             if <tt>to > data.Length</tt>
        // throws System.ArgumentException
        //             if <tt>from < 0</tt> or <tt>to > data.Length</tt>
        // throws NullPointerException
        //             if {data} is null or {coeffs} is null
        // 
        public float[] smooth(float[] data, int from, int to, double[] coeffs)
        {
            return smooth(data, from, to, 0, new double[][] { coeffs });
        }

        // 
        // See {#smooth(double[], int, int, int, double[][])}. This method
        // converts {data} to double for computation and then converts it back
        // to float
        // 
        // input data
        //            data for filter
        // input from
        //            inedx of the first element of data
        // input to
        //            index of the first element omitted
        // input bias
        //            how many points of pad should be left out when smoothing
        // input coeffs
        //            filter coefficients
        // return filtered data
        // throws ArrayIndexOutOfBoundsException
        //             if <tt>to > data.Length</tt> or when {coeffs} has less
        //             than <tt>2*bias + 1</tt> elements
        // throws System.ArgumentException
        //             if <tt>from < 0</tt> or <tt>to > data.Length</tt> or
        //             <tt>from > to</tt> or when <tt>bias < 0</tt> or
        //             <tt>bias > min(nr, nl)</tt>
        // throws NullPointerException
        //             if {data} is null or {coeffs} is null
        // 
        public float[] smooth(float[] data, int from, int to, int bias, double[][] coeffs)
        {
            // float[] leftPad = Arrays.copyOfRange(data, 0, from);
            float[] leftPad = new float[from];
            Array.Copy(leftPad, 0, data, 0, from);
            //float[] rightPad = Arrays.copyOfRange(data, to, data.Length);
            float[] rightPad = new float[data.Length - to];
            Array.Copy(rightPad, 0, data, to, data.Length);
            //float[] dataCopy = Arrays.copyOfRange(data, from, to);
            float[] dataCopy = new float[to - from];
            Array.Copy(dataCopy, 0, data, from, to);
            return smooth(dataCopy, leftPad, rightPad, bias, coeffs);
        }
    }

    public class ContinuousPadder : Preprocessor
    {

        private bool paddingLeft = true;

        private bool paddingRight = true;

        // 
        // Default constructor. Both left and right padding are turned on
        // 
        public ContinuousPadder()
        {

        }

        // 
        // 
        // inputpaddingLeft
        //            enables or disables left padding
        // inputpaddingRight
        //            enables or disables right padding
        // 
        public ContinuousPadder(bool paddingLeft, bool paddingRight)
        {
            this.paddingLeft = paddingLeft;
            this.paddingRight = paddingRight;
        }

        public void apply(double[] data)
        {
            int n = data.Length;
            if (paddingLeft)
            {
                int l = 0;
                // seek first non-zero cell
                for (int i = 0; i < n; i++)
                {
                    if (data[i] != 0)
                    {
                        l = i;
                        break;
                    }
                }
                double y0 = data[l];
                for (int i = 0; i < l; i++)
                {
                    data[i] = y0;
                }
            }
            if (paddingRight)
            {
                int r = 0;
                // seek last non-zero cell
                for (int i = n - 1; i >= 0; i--)
                {
                    if (data[i] != 0)
                    {
                        r = i;
                        break;
                    }
                }
                double ynr = data[r];
                for (int i = r + 1; i < n; i++)
                {
                    data[i] = ynr;
                }
            }
        }

        // 
        // 
        // return { paddingLeft}
        // 
        public bool isPaddingLeft()
        {
            return paddingLeft;
        }

        // 
        // 
        // return { paddingRight}
        // 
        public bool isPaddingRight()
        {
            return paddingRight;
        }

        // 
        // 
        // inputpaddingLeft
        //            enables or disables left padding
        // 
        public void setPaddingLeft(bool paddingLeft)
        {
            this.paddingLeft = paddingLeft;
        }

        // 
        // 
        // inputpaddingRight
        //            enables or disables right padding
        // 
        public void setPaddingRight(bool paddingRight)
        {
            this.paddingRight = paddingRight;
        }

    }

    // 
    // Linearizes data by seeking points with relative difference greater than
    // {#getTruncateRatio() truncateRatio} and replacing them with points
    // lying on line between the first and the last of such points. Strictly:
    // <p>
    // let <tt>delta(i)</tt> be function which assigns to an element at index
    // <tt>i (data[i])</tt>, for <tt>0
    // <= i < data.Length - 1</tt>, value of
    // <tt>|(data[i] - data[i+1])/data[i]|</tt>. Then for each range <tt>(j,k)</tt>
    // of data, such that
    // <tt>delta(j) > {#getTruncateRatio() truncateRatio}</tt> and
    // <tt>delta(k)
    // <= {#getTruncateRatio() truncateRatio}</tt>, <tt>data[x] = ((data[k] -
    // data[j])/(k - j)) * (x - k) + data[j])</tt> for <tt>j <= x <= k</tt>.
    // </p>
    // 
    // 
    // 
    public class Linearizer : Preprocessor
    {

        private float truncateRatio = 0.5f;

        // 
        // Default constructor. {#getTruncateRatio() truncateRatio} is 0.5
        // 
        public Linearizer()
        {

        }

        // 
        // 
        // inputtruncateRatio
        //            maximum relative difference of subsequent data points above
        //            which linearization begins
        // throws System.ArgumentException
        //             when { truncateRatio} < 0
        // 
        public Linearizer(float truncateRatio)
        {
            if (truncateRatio < 0f)
                throw new System.ArgumentException("truncateRatio < 0");
            this.truncateRatio = truncateRatio;
        }

        public void apply(double[] data)
        {
            int n = data.Length - 1;
            double[] deltas = computeDeltas(data);
            bool end;
            for (int i = 0; i < n; i++)
            { // linreg: for (int i = 0; i < n; i++) {
                if (deltas[i] > truncateRatio)
                {
                    end = false;
                    for (int k = i + 1; k < n; k++)
                    {
                        if (deltas[k] <= truncateRatio)
                        {
                            linest(data, i, k);
                            i = k - 1;
                            end = true;
                            //continue linreg;
                        }
                    }
                    if (end == true)
                        continue;
                    linest(data, i, n);
                    break;
                }
            }
        }

        protected double[] computeDeltas(double[] data)
        {
            int n = data.Length;
            double[] deltas = new double[n - 1];
            for (int i = 0; i < n - 1; i++)
            {
                if (data[i] == 0 && data[i + 1] == 0)
                {
                    deltas[i] = 0;
                }
                else
                {
                    deltas[i] = Math.Abs(1 - data[i + 1] / data[i]);
                }
            }
            return deltas;
        }

        // 
        // 
        // return { truncateRatio}
        // 
        public float getTruncateRatio()
        {
            return truncateRatio;
        }

        protected void linest(double[] data, int x0, int x1)
        {
            if (x0 + 1 == x1)
                return;
            double slope = (data[x1] - data[x0]) / (x1 - x0);
            double y0 = data[x0];
            for (int x = x0 + 1; x < x1; x++)
            {
                data[x] = (slope * (x - x0) + y0);
            }
        }

        // 
        // 
        // inputtruncateRatio
        //            maximum relative difference of subsequent data points above
        //            which linearization begins
        // throws System.ArgumentException
        //             when { truncateRatio} < 0
        // 
        public void setTruncateRatio(float truncateRatio)
        {
            if (truncateRatio < 0f)
                throw new System.ArgumentException("truncateRatio < 0");
            this.truncateRatio = truncateRatio;
        }

    }

    // 
    // Pads data to left and/or right.:
    // 
    // <p>
    // <ul>
    // <li>
    // Let <tt>l</tt> be the index of the first non-zero element in data (for left
    // padding),</li>
    // <li>let <tt>r</tt> be the index of the last non-zero element in data (for
    // right padding)</li>
    // </ul>
    // then for every element <tt>e</tt> which index is <tt>i</tt> such that:
    // <ul>
    // <li>
    // <tt>0 <= i < l</tt>, <tt>e</tt> is replaced with arithmetic mean of
    // <tt>data[l]..data[l + window_length/2 - 1]</tt> (left padding)</li>
    // <li>
    // <tt>r < i < data.Length</tt>, <tt>e</tt> is replaced with arithmetic mean of
    // <tt>data[r - window_length/2 + 1]..data[r]</tt> (right padding)</li>
    // </ul>
    // </p>
    // Example:
    // <p>
    // Given data: <tt>[0,0,0,1,2,1,3,1,2,4,0]</tt> result of applying
    // MeanValuePadder with {#getWindowLength() window_length} = 4 is:
    // <tt>[1.5,1.5,1.5,1,2,1,3,1,2,4,0]</tt> in case of {#isPaddingLeft()
    // left padding}; <tt>[0,0,0,1,2,1,3,1,2,4,3]</tt> in case of
    // {#isPaddingRight() right padding};
    // </p>
    // 
    // 
    // 
    public class MeanValuePadder : Preprocessor
    {

        private bool paddingLeft = true;

        private bool paddingRight = true;

        private int windowLength;

        // 
        // 
        // inputwindowLength
        //            window length of filter which will be used to smooth data.
        //            Padding will use half of { windowLength} length. In this
        //            way padding will be suited to smoothing operation
        // throws System.ArgumentException
        //             if { windowLength} < 0
        // 
        public MeanValuePadder(int windowLength)
        {
            if (windowLength < 0)
                throw new System.ArgumentException("windowLength < 0");
            this.windowLength = windowLength;
        }

        // 
        // 
        // inputwindowLength
        //            window length of filter which will be used to smooth data.
        //            Padding will use half of { windowLength} length. In this
        //            way padding will be suited to smoothing operation
        // inputpaddingLeft
        //            enables or disables left padding
        // inputpaddingRight
        //            enables or disables left padding
        // throws System.ArgumentException
        //             if { windowLength} < 0
        // 
        public MeanValuePadder(int windowLength, bool paddingLeft,
                bool paddingRight)
        {
            if (windowLength < 0)
                throw new System.ArgumentException("windowLength < 0");
            this.windowLength = windowLength;
            this.paddingLeft = paddingLeft;
            this.paddingRight = paddingRight;
        }

        public void apply(double[] data)
        {
            // padding values with average of last (WINDOW_LENGTH / 2) points
            int n = data.Length;
            if (paddingLeft)
            {
                int l = 0;
                // seek first non-zero cell
                for (int i = 0; i < n; i++)
                {
                    if (data[i] != 0)
                    {
                        l = i;
                        break;
                    }
                }
                double avg = 0;
                int m = Math.Min(l + windowLength / 2, n);
                for (int i = l; i < m; i++)
                {
                    avg += data[i];
                }
                avg /= (m - l);
                for (int i = 0; i < l; i++)
                {
                    data[i] = avg;
                }
            }
            if (paddingRight)
            {
                int r = 0;
                // seek last non-zero cell
                for (int i = n - 1; i >= 0; i--)
                {
                    if (data[i] != 0)
                    {
                        r = i;
                        break;
                    }
                }
                double avg = 0;
                int m = Math.Min(windowLength / 2, r + 1);
                for (int i = 0; i < m; i++)
                {
                    avg += data[r - i];
                }
                avg /= m;
                for (int i = r + 1; i < n; i++)
                {
                    data[i] = avg;
                }
            }
        }

        // 
        // 
        // return { windowLength}
        // 
        public int getWindowLength()
        {
            return windowLength;
        }

        // 
        // 
        // return { paddingLeft}
        // 
        public bool isPaddingLeft()
        {
            return paddingLeft;
        }

        // 
        // 
        // return { paddingRight}
        // 
        public bool isPaddingRight()
        {
            return paddingRight;
        }

        // 
        // 
        // inputpaddingLeft
        //            enables or disables left padding
        // 
        public void setPaddingLeft(bool paddingLeft)
        {
            this.paddingLeft = paddingLeft;
        }

        // 
        // 
        // inputpaddingRight
        //            enables or disables right padding
        // 
        public void setPaddingRight(bool paddingRight)
        {
            this.paddingRight = paddingRight;
        }

        // 
        // 
        // inputwindowLength
        // throws System.ArgumentException
        //             if { windowLength} < 0
        // 
        public void setWindowLength(int windowLength)
        {
            if (windowLength < 0)
                throw new System.ArgumentException("windowLength < 0");
            this.windowLength = windowLength;
        }

    }

    // 
    // Filters data using Ramer-Douglas-Peucker algorithm with specified tolerance
    // 
    // see <a href="http://en.wikipedia.org/wiki/Ramer-Douglas-Peucker_algorithm">Ramer-Douglas-Peucker algorithm</a>
    // 
    public class RamerDouglasPeuckerFilter : DataFilter
    {

        private double epsilon;

        // 
        // 
        // inputepsilon
        //            epsilon in Ramer-Douglas-Peucker algorithm (maximum distance
        //            of a point in data between original curve and simplified
        //            curve)
        // throws System.ArgumentException
        //             when { epsilon <= 0}
        // 
        public RamerDouglasPeuckerFilter(double epsilon)
        {
            if (epsilon <= 0)
            {
                throw new System.ArgumentException("Epsilon nust be > 0");
            }
            this.epsilon = epsilon;
        }

        public double[] filter(double[] data)
        {
            return ramerDouglasPeuckerFunction(data, 0, data.Length - 1);
        }

        // 
        // 
        // return { epsilon}
        // 
        public double getEpsilon()
        {
            return epsilon;
        }

        protected double[] ramerDouglasPeuckerFunction(double[] points,
                        int startIndex, int endIndex)
        {
            double dmax = 0;
            int idx = 0;
            double a = endIndex - startIndex;
            double b = points[endIndex] - points[startIndex];
            double c = -(b * startIndex - a * points[startIndex]);
            double norm = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            for (int i = startIndex + 1; i < endIndex; i++)
            {
                double distance = Math.Abs(b * i - a * points[i] + c) / norm;
                if (distance > dmax)
                {
                    idx = i;
                    dmax = distance;
                }
            }
            if (dmax >= epsilon)
            {
                double[] recursiveResult1 = ramerDouglasPeuckerFunction(points,
                                startIndex, idx);
                double[] recursiveResult2 = ramerDouglasPeuckerFunction(points,
                                idx, endIndex);
                double[] result = new double[(recursiveResult1.Length - 1)
                                + recursiveResult2.Length];
                Array.Copy(recursiveResult1, 0, result, 0, recursiveResult1.Length - 1);
                //System.arraycopy(recursiveResult1, 0, result, 0, recursiveResult1.Length - 1);
                Array.Copy(recursiveResult2, 0, result, recursiveResult1.Length - 1, recursiveResult2.Length);
                //System.arraycopy(recursiveResult2, 0, result, recursiveResult1.Length - 1, recursiveResult2.Length);

                return result;
            }
            else
            {
                return new double[] { points[startIndex], points[endIndex] };
            }
        }

        // 
        // 
        // inputepsilon
        //            maximum distance of a point in data between original curve and
        //            simplified curve
        // 
        public void setEpsilon(double epsilon)
        {
            if (epsilon <= 0)
            {
                throw new System.ArgumentException("Epsilon nust be > 0");
            }
            this.epsilon = epsilon;
        }

    }

    // 
    // De-trends data by setting straight line between the first and the last point
    // and subtracting it from data. Having applied filters to data you should
    // reverse detrending by using {TrendRemover#retrend(double[], double[])}
    // 
    // @author Marcin RzeÅºnicki
    // 
    // 
    public class TrendRemover : Preprocessor
    {

        public void apply(double[] data)
        {
            // de-trend data so to avoid boundary distortion
            // we will achieve this by setting straight line from end to beginning
            // and subtracting it from the trend
            int n = data.Length;
            if (n <= 2)
                return;
            double y0 = data[0];
            double slope = (data[n - 1] - y0) / (n - 1);
            for (int x = 0; x < n; x++)
            {
                data[x] -= (slope * x + y0);
            }
        }

        // 
        // Reverses the effect of {#apply(double[])} by modifying {@code
        // newData}
        // 
        // inputnewData
        //            processed data
        // inputdata
        //            original data
        // 
        public void retrend(double[] newData, double[] data)
        {
            int n = data.Length;
            double y0 = data[0];
            double slope = (data[n - 1] - y0) / (n - 1);
            for (int x = 0; x < n; x++)
            {
                newData[x] += slope * x + y0;
            }
        }

    }

    // 
    // Eliminates zeros from data - starting from the first non-zero element, ending
    // at the last non-zero element. More specifically:
    // <p>
    // <ul>
    // <li>
    // Let <tt>l</tt> be the index of the first non-zero element in data,</li>
    // <li>let <tt>r</tt> be the index of the last non-zero element in data</li>
    // </ul>
    // then for every element <tt>e</tt> which index is <tt>i</tt> such that:
    // <tt>l < i < r</tt> and <tt>e == 0</tt>, <tt>e</tt> is replaced with element <tt>e'</tt>
    // with index <tt>j</tt> such that:
    // <ul>
    // <li><tt>l <= j < i</tt> and <tt>e' <> 0</tt> and for all indexes
    // <tt>k: j < k < i; e[k] == 0</tt> - when {#isAlignToLeft() alignToLeft}
    // is true</li>
    // <li><tt>i < j <= r</tt> and <tt>e' <> 0</tt> and for all indexes
    // <tt>k: i < k < j;e[k] == 0</tt> - otherwise</li>
    // </ul>
    // </p>
    // Example:
    // <p>
    // Given data: <tt>[0,0,0,1,2,0,3,0,0,4,0]</tt> result of applying
    // ZeroEliminator is: <tt>[0,0,0,1,2,2,3,3,3,4,0]</tt> if
    // {#isAlignToLeft() alignToLeft} is true;
    // <tt>[0,0,0,1,2,3,3,4,4,4,0]</tt> - otherwise
    // </p>
    // 
    // 
    // 
    public class ZeroEliminator : Preprocessor
    {

        private bool alignToLeft;

        // 
        // Default constructor: {alignToLeft} is {false}
        // 
        // see #ZeroEliminator(bool)
        // 
        public ZeroEliminator()
        {

        }

        // 
        // 
        // inputalignToLeft
        //            if {true} zeros will be replaced with non-zero element
        //            to the left, if {false} - to the right
        // 
        public ZeroEliminator(bool alignToLeft)
        {
            this.alignToLeft = alignToLeft;
        }

        public void apply(double[] data)
        {
            int n = data.Length;
            int l = 0, r = 0;
            // seek first non-zero cell
            for (int i = 0; i < n; i++)
            {
                if (data[i] != 0)
                {
                    l = i;
                    break;
                }
            }
            // seek last non-zero cell
            for (int i = n - 1; i >= 0; i--)
            {
                if (data[i] != 0)
                {
                    r = i;
                    break;
                }
            }
            // eliminate 0s
            if (alignToLeft)
                for (int i = l + 1; i < r; i++)
                {
                    if (data[i] == 0)
                    {
                        data[i] = data[i - 1];
                    }
                }
            else
                for (int i = r - 1; i > l; i--)
                {
                    if (data[i] == 0)
                    {
                        data[i] = data[i + 1];
                    }
                }
        }

        // 
        // 
        // return {alignToLeft}
        // 
        public bool isAlignToLeft()
        {
            return alignToLeft;
        }

        // 
        // 
        // inputalignToLeft
        //            if {true} zeros will be replaced with non-zero element
        //            to the left, if {false} - to the right
        // 
        public void setAlignToLeft(bool alignToLeft)
        {
            this.alignToLeft = alignToLeft;
        }

    }
}