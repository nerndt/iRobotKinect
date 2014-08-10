/// <summary>BitmapManipulator class, provides some useful static functions which
/// operate on .NET <code>Bitmap</code> objects in useful ways.
/// 
/// Some of the useful features of this class incldue:
/// <ul>
///   <li><code>GetBitmapFromUri</code> which downloads a bitmap from a URL, providing
///   some useful error message elaboration logic to present users with a more meaningful
///   error message in the event of a failure.</li>
/// 
///   <li><code>ConvertBitmap</code> functions, which convert a bitmap from one format
///   to another, including optional quality and compression parameters for codecs like JPEG and
///   TIFF, respectively.</li>
/// 
///   <li><code>ScaleBitmap</code> and <code>ResizeBitmap</code>, for modifying the dimensions
///   of a bitmap (these are standard issue and boring, but nonetheless useful)</li>
/// 
///   <li><code>ThumbnailBitmap</code>, a very useful function that produces a thumbnail of an image
///   that fits within a given rectangle</li>
/// 
///   <li><code>OverlayBitmap</code>, a useful function that overlays one bitmap atop another
///   with a caller-defined alpha parameter.  Great for putting watermarks or logos on pictures.</li>
/// 
///   <li>A few other standard-issue image manipulation functions</li>
/// </ul>
/// 
/// NOTE: This code includes support for GIF en/decoding, via the .NET Framework's
/// System.Drawing classes.  However, in order to provide GIF functionality in your
/// application, you must license the LZW encoding scheme used in GIF files from Unisys.
/// As this is an opportunistic money-grab akin to SCO's, you are well advised to refuse
/// to do this, and instead favor PNG whenever possible.
/// 
/// For more information, see http://www.microsoft.com/DEVONLY/Unisys.htm
/// 
/// Current Version: 1.0.0
/// Revision History:
/// 1.0.0 - ajn - 9/1/2003 - First release
/// 
/// Copyright(C) 2003 Adam J. Nelson.
/// 
/// This code is hereby released for unlimited non-commercial and commercial use
/// 
/// The author makes no guarantee regarding the fitness of this code, and hereby disclaims
/// all liability for any damage incurred as a result of using this code.
/// </summary>
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Linq;
using System.Runtime.InteropServices; // for ChangeColor Methods
using System.Collections.Generic;

namespace iRobotKinect
{
    /// <summary>
    /// Utility class with static methods that do various useful things
    /// with bitmaps that require multiple GDI+ calls with .NET CLR
    /// </summary>
    public class BitmapManipulator
    {
        // NOTE: Bitmap.MakeTransparent() can change any specific color in the bitmap to a transparent color

        //MIME types for the various image formats
        private const String MIME_JPEG = "image/jpeg";
        private const String MIME_PJPEG = "image/pjpeg";
        private const String MIME_GIF = "image/gif";
        private const String MIME_BMP = "image/bmp";
        private const String MIME_TIFF = "image/tiff";
        private const String MIME_PNG = "image/x-png";

        public class BitmapManipException : Exception
        {
            public BitmapManipException(String msg, Exception innerException)
                : base(msg, innerException)
            {
            }
        }

        public enum ImageCornerEnum
        {
            TopLeft,
            TopRight,
            BottomRight,
            BottomLeft,
            Center
        };

        public enum TiffCompressionEnum
        {
            CCITT3,
            CCITT4,
            LZW,
            RLE,
            None,
            Unspecified
        };

        public static String[] supportedMimeTypes = new String[] {                                                              
			MIME_GIF,                                                                  
				MIME_JPEG,
				MIME_PJPEG,
				MIME_TIFF,
				MIME_PNG,
				MIME_BMP
		};

        /// <summary>Attempts to download a bitmap from a given URI, then loads the bitmap into
        /// a <code>Bitmap</code> object and returns.
        /// 
        /// Obviously there are numerous failure cases for a function like this.  For ease 
        /// of use, all errors will be reported in a catch-all <code>BitmapManipException</code>,
        /// which provides a textual error message based on the exception that occurs.  As usual,
        /// the underlying exception is available in <code>InnerException</code> property.
        /// 
        /// Times out after 10 seconds waiting for a response from the server.</summary>
        /// 
        /// <param name="uri">String containing URI from which to retrieve image</param>
        /// 
        /// <returns>Bitmap object from URI.  Shouldn't ever be null, as any error will be reported
        ///     in an exception.</returns>
        public static Bitmap GetBitmapFromUri(String uri)
        {
            //Convert String to URI
            try
            {
                Uri uriObj = new Uri(uri);

                return GetBitmapFromUri(uriObj);
            }
            catch (ArgumentNullException ex)
            {
                throw new BitmapManipException("Parameter 'uri' is null", ex);
            }
            catch (UriFormatException ex)
            {
                throw new BitmapManipException(String.Format("Parameter 'uri' is malformed: {0}", ex.Message),
                                               ex);
            }
        }
        /// <summary>Attempts to download a bitmap from a given URI, then loads the bitmap into
        /// a <code>Bitmap</code> object and returns.
        /// 
        /// Obviously there are numerous failure cases for a function like this.  For ease
        /// of use, all errors will be reported in a catch-all <code>BitmapManipException</code>,
        /// which provides a textual error message based on the exception that occurs.  As usual,
        /// the underlying exception is available in <code>InnerException</code> property.
        /// 
        /// Times out after 10 seconds waiting for a response from the server.</summary>
        /// 
        /// <param name="uri"><code>Uri</code> object specifying the URI from which to retrieve image</param>
        /// 
        /// <returns>Bitmap object from URI.  Shouldn't ever be null, as any error will be reported
        ///     in an exception.</returns>
        public static Bitmap GetBitmapFromUri(Uri uri)
        {
            return GetBitmapFromUri(uri, 10 * 1000);
        }

        /// <summary>Attempts to download a bitmap from a given URI, then loads the bitmap into
        /// a <code>Bitmap</code> object and returns.
        /// 
        /// Obviously there are numerous failure cases for a function like this.  For ease 
        /// of use, all errors will be reported in a catch-all <code>BitmapManipException</code>,
        /// which provides a textual error message based on the exception that occurs.  As usual,
        /// the underlying exception is available in <code>InnerException</code> property.
        /// </summary>
        /// 
        /// <param name="uri">String containing URI from which to retrieve image</param>
        /// <param name="timeoutMs">Timeout (in milliseconds) to wait for response</param>
        /// 
        /// <returns>Bitmap object from URI.  Shouldn't ever be null, as any error will be reported
        ///     in an exception.</returns>
        public static Bitmap GetBitmapFromUri(String uri, int timeoutMs)
        {
            //Convert String to URI
            try
            {
                Uri uriObj = new Uri(uri);

                return GetBitmapFromUri(uriObj, timeoutMs);
            }
            catch (ArgumentNullException ex)
            {
                throw new BitmapManipException("Parameter 'uri' is null", ex);
            }
            catch (UriFormatException ex)
            {
                throw new BitmapManipException(String.Format("Parameter 'uri' is malformed: {0}", ex.Message),
                                               ex);
            }
        }

        /// <summary>Attempts to download a bitmap from a given URI, then loads the bitmap into
        /// a <code>Bitmap</code> object and returns.
        /// 
        /// Obviously there are numerous failure cases for a function like this.  For ease
        /// of use, all errors will be reported in a catch-all <code>BitmapManipException</code>,
        /// which provides a textual error message based on the exception that occurs.  As usual,
        /// the underlying exception is available in <code>InnerException</code> property.
        /// </summary>
        /// 
        /// <param name="uri"><code>Uri</code> object specifying the URI from which to retrieve image</param>
        /// <param name="timeoutMs">Timeout (in milliseconds) to wait for response</param>
        /// 
        /// <returns>Bitmap object from URI.  Shouldn't ever be null, as any error will be reported
        ///     in an exception.</returns>
        public static Bitmap GetBitmapFromUri(Uri uri, int timeoutMs)
        {
            Bitmap downloadedImage = null;

            //Create a web request object for the URI, retrieve the contents,
            //then feed the results into a new Bitmap object.  Note that we 
            //are particularly sensitive to timeouts, since this all must happen
            //while the user waits
            try
            {
                WebRequest req = WebRequest.Create(uri);
                req.Timeout = timeoutMs;

                //The GetResponse call actually makes the request
                WebResponse resp = req.GetResponse();

                //Check the content type of the response to make sure it is
                //one of the formats we support
                if (Array.IndexOf(BitmapManipulator.supportedMimeTypes,
                                  resp.ContentType) == -1)
                {
                    String contentType = resp.ContentType;
                    resp.Close();
                    throw new BitmapManipException(String.Format("The image at the URL you provided is in an unsupported format ({0}).  Uploaded images must be in either JPEG, GIF, BMP, TIFF, PNG, or WMF formats.",
                                                                 contentType),
                                                   new NotSupportedException(String.Format("MIME type '{0}' is not a recognized image type", contentType)));
                }

                //Otherwise, looks fine
                downloadedImage = new Bitmap(resp.GetResponseStream());

                resp.Close();

                return downloadedImage;
            }
            catch (UriFormatException exp)
            {
                throw new BitmapManipException("The URL you entered is not valid.  Please enter a valid URL, of the form http://servername.com/folder/image.gif",
                                               exp);
            }
            catch (WebException exp)
            {
                //Some sort of problem w/ the web request
                String errorDescription;

                if (exp.Status == WebExceptionStatus.ConnectFailure)
                {
                    errorDescription = "Connect failure";
                }
                else if (exp.Status == WebExceptionStatus.ConnectionClosed)
                {
                    errorDescription = "Connection closed prematurely";
                }
                else if (exp.Status == WebExceptionStatus.KeepAliveFailure)
                {
                    errorDescription = "Connection closed in spite of keep-alives";
                }
                else if (exp.Status == WebExceptionStatus.NameResolutionFailure)
                {
                    errorDescription = "Unable to resolve server name.  Double-check the URL for errors";
                }
                else if (exp.Status == WebExceptionStatus.ProtocolError)
                {
                    errorDescription = "Protocol-level error.  The server may have reported an error like 404 (file not found) or 403 (access denied), or some other similar error";
                }
                else if (exp.Status == WebExceptionStatus.ReceiveFailure)
                {
                    errorDescription = "The server did not send a complete response";
                }
                else if (exp.Status == WebExceptionStatus.SendFailure)
                {
                    errorDescription = "The complete request could not be sent to the server";
                }
                else if (exp.Status == WebExceptionStatus.ServerProtocolViolation)
                {
                    errorDescription = "The server response was not a valid HTTP response";
                }
                else if (exp.Status == WebExceptionStatus.Timeout)
                {
                    errorDescription = "The server did not respond quickly enough.  The server may be down or overloaded.  Try again later";
                }
                else
                {
                    errorDescription = exp.Status.ToString();
                }

                throw new BitmapManipException(String.Format("An error occurred while communicating with the server at the URL you provided.  {0}.",
                                                             errorDescription),
                                               exp);
            }
            catch (BitmapManipException exp)
            {
                //Don't modify this one; pass it along
                throw exp;
            }
            catch (Exception exp)
            {
                throw new BitmapManipException(String.Format("An error ocurred while retrieving the image from the URL you provided: {0}",
                                                             exp.Message),
                                               exp);
            }
        }

        /// <summary>Converts a bitmap to a JPEG with a specific quality level</summary>
        /// 
        /// <param name="inputBmp">Bitmap to convert</param>
        /// <param name="quality">Specifies a quality from 0 (lowest) to 100 (highest), or -1 to leave
        /// unspecified</param>
        /// 
        /// <returns>A new bitmap object containing the input bitmap converted.
        ///     If the destination format and the target format are the same, returns
        ///     a clone of the destination bitmap.</returns>
        public static Bitmap ConvertBitmapToJpeg(Bitmap inputBmp, int quality)
        {
            //If the dest format matches the source format and quality not changing, just clone
            if (inputBmp.RawFormat.Equals(ImageFormat.Jpeg) && quality == -1)
            {
                return (Bitmap)inputBmp.Clone();
            }

            //Create an in-memory stream which will be used to save
            //the converted image
            System.IO.Stream imgStream = new System.IO.MemoryStream();

            //Get the ImageCodecInfo for the desired target format
            ImageCodecInfo destCodec = FindCodecForType(MimeTypeFromImageFormat(ImageFormat.Jpeg));

            if (destCodec == null)
            {
                //No codec available for that format
                throw new ArgumentException("The requested format " +
                                            MimeTypeFromImageFormat(ImageFormat.Jpeg) +
                                            " does not have an available codec installed",
                                            "destFormat");
            }

            //Create an EncoderParameters collection to contain the
            //parameters that control the dest format's encoder
            EncoderParameters destEncParams = new EncoderParameters(1);

            //Use quality parameter
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);
            destEncParams.Param[0] = qualityParam;

            //Save w/ the selected codec and encoder parameters
            inputBmp.Save(imgStream, destCodec, destEncParams);

            //At this point, imgStream contains the binary form of the
            //bitmap in the target format.  All that remains is to load it
            //into a new bitmap object
            Bitmap destBitmap = new Bitmap(imgStream);

            //Free the stream
            //imgStream.Close();
            //For some reason, the above causes unhandled GDI+ exceptions
            //when destBitmap.Save is called.  Perhaps the bitmap object reads
            //from the stream asynchronously?

            return destBitmap;
        }

        /// <summary>Converts a bitmap to a Tiff with a specific compression</summary>
        /// 
        /// <param name="inputBmp">Bitmap to convert</param>
        /// <param name="compression">The compression to use on the TIFF file output.  Be warned that the CCITT3, CCITT4,
        ///     and RLE compression options are only applicable to TIFFs using a palette index color depth 
        ///     (that is, 1, 4, or 8 bpp).  Using any of these compression schemes with 24 or 32-bit 
        ///     TIFFs will throw an exception from the bowels of GDI+</param>
        /// 
        /// <returns>A new bitmap object containing the input bitmap converted.
        ///     If the destination format and the target format are the same, returns
        ///     a clone of the destination bitmap.</returns>
        public static Bitmap ConvertBitmapToTiff(Bitmap inputBmp, TiffCompressionEnum compression)
        {
            //If the dest format matches the source format and quality/bpp not changing, just clone
            if (inputBmp.RawFormat.Equals(ImageFormat.Tiff) && compression == TiffCompressionEnum.Unspecified)
            {
                return (Bitmap)inputBmp.Clone();
            }

            if (compression == TiffCompressionEnum.Unspecified)
            {
                //None of the params are chaning; use the general purpose converter
                return ConvertBitmap(inputBmp, ImageFormat.Tiff);
            }

            //Create an in-memory stream which will be used to save
            //the converted image
            System.IO.Stream imgStream = new System.IO.MemoryStream();

            //Get the ImageCodecInfo for the desired target format
            ImageCodecInfo destCodec = FindCodecForType(MimeTypeFromImageFormat(ImageFormat.Tiff));

            if (destCodec == null)
            {
                //No codec available for that format
                throw new ArgumentException("The requested format " +
                                            MimeTypeFromImageFormat(ImageFormat.Tiff) +
                                            " does not have an available codec installed",
                                            "destFormat");
            }


            //Create an EncoderParameters collection to contain the
            //parameters that control the dest format's encoder
            EncoderParameters destEncParams = new EncoderParameters(1);

            //set the compression parameter
            EncoderValue compressionValue;

            switch (compression)
            {
                case TiffCompressionEnum.CCITT3:
                    compressionValue = EncoderValue.CompressionCCITT3;
                    break;

                case TiffCompressionEnum.CCITT4:
                    compressionValue = EncoderValue.CompressionCCITT4;
                    break;

                case TiffCompressionEnum.LZW:
                    compressionValue = EncoderValue.CompressionLZW;
                    break;

                case TiffCompressionEnum.RLE:
                    compressionValue = EncoderValue.CompressionRle;
                    break;

                default:
                    compressionValue = EncoderValue.CompressionNone;
                    break;
            }
            EncoderParameter compressionParam = new EncoderParameter(Encoder.Compression, (long)compressionValue);

            destEncParams.Param[0] = compressionParam;

            //Save w/ the selected codec and encoder parameters
            inputBmp.Save(imgStream, destCodec, destEncParams);

            //At this point, imgStream contains the binary form of the
            //bitmap in the target format.  All that remains is to load it
            //into a new bitmap object
            Bitmap destBitmap = new Bitmap(imgStream);

            //Free the stream
            //imgStream.Close();
            //For some reason, the above causes unhandled GDI+ exceptions
            //when destBitmap.Save is called.  Perhaps the bitmap object reads
            //from the stream asynchronously?

            return destBitmap;
        }

        /// <summary>Converts a bitmap to another bitmap format, returning the new converted
        ///     bitmap
        /// </summary>
        /// 
        /// <param name="inputBmp">Bitmap to convert</param>
        /// <param name="destMimeType">MIME type of format to convert to</param>
        /// 
        /// <returns>A new bitmap object containing the input bitmap converted.
        ///     If the destination format and the target format are the same, returns
        ///     a clone of the destination bitmap.</returns>
        public static Bitmap ConvertBitmap(Bitmap inputBmp, String destMimeType)
        {
            return ConvertBitmap(inputBmp, ImageFormatFromMimeType(destMimeType));
        }

        /// <summary>Converts a bitmap to another bitmap format, returning the new converted
        ///     bitmap
        /// </summary>
        /// 
        /// <param name="inputBmp">Bitmap to convert</param>
        /// <param name="destFormat">Bitmap format to convert to</param>
        /// 
        /// <returns>A new bitmap object containing the input bitmap converted.
        ///     If the destination format and the target format are the same, returns
        ///     a clone of the destination bitmap.</returns>
        public static Bitmap ConvertBitmap(Bitmap inputBmp, System.Drawing.Imaging.ImageFormat destFormat)
        {
            //If the dest format matches the source format and quality/bpp not changing, just clone
            if (inputBmp.RawFormat.Equals(destFormat))
            {
                return (Bitmap)inputBmp.Clone();
            }

            //Create an in-memory stream which will be used to save
            //the converted image
            System.IO.Stream imgStream = new System.IO.MemoryStream();

            //Save the bitmap out to the memory stream, using the format indicated by the caller
            inputBmp.Save(imgStream, destFormat);

            //At this point, imgStream contains the binary form of the
            //bitmap in the target format.  All that remains is to load it
            //into a new bitmap object
            Bitmap destBitmap = new Bitmap(imgStream);

            //Free the stream
            //imgStream.Close();
            //For some reason, the above causes unhandled GDI+ exceptions
            //when destBitmap.Save is called.  Perhaps the bitmap object reads
            //from the stream asynchronously?

            return destBitmap;
        }

        /// <summary>
        /// Scales a bitmap by a scale factor, growing or shrinking both axes while
        /// maintaining the original aspect ratio
        /// </summary>
        /// <param name="inputBmp">Bitmap to scale</param>
        /// <param name="scaleFactor">Factor by which to scale</param>
        /// <returns>New bitmap containing image from inputBmp, scaled by the scale factor</returns>
        public static Bitmap ScaleBitmap(Bitmap inputBmp, double scaleFactor, InterpolationMode interpolationMode = InterpolationMode.HighQualityBicubic, PixelFormat pixelFormat = PixelFormat.Format32bppArgb)
        {
            return ScaleBitmap(inputBmp, scaleFactor, scaleFactor, interpolationMode, pixelFormat);
        }

        /// <summary>
        /// Scales a bitmap by a scale factor, growing or shrinking both axes independently, 
        /// possibly changing the aspect ration
        /// </summary>
        /// <param name="inputBmp">Bitmap to scale</param>
        /// <param name="scaleFactor">Factor by which to scale</param>
        /// <returns>New bitmap containing image from inputBmp, scaled by the scale factor</returns>
        public static Bitmap ScaleBitmap(Bitmap inputBmp, double xScaleFactor, double yScaleFactor, InterpolationMode interpolationMode = InterpolationMode.HighQualityBicubic, PixelFormat pixelFormat = PixelFormat.Format32bppArgb)
        {
            //Create a new bitmap object based on the input
            Bitmap newBmp = new Bitmap(
                                      (int)(inputBmp.Size.Width * xScaleFactor),
                                      (int)(inputBmp.Size.Height * yScaleFactor),
                                      pixelFormat); // PixelFormat.Format24bppRgb);//Graphics.FromImage doesn't like Indexed pixel format

            //Create a graphics object attached to the new bitmap
            Graphics newBmpGraphics = Graphics.FromImage(newBmp);

            newBmpGraphics.SmoothingMode = SmoothingMode.HighQuality; // Make sure highest quality 06132013 
            //Set the interpolation mode to high quality bicubic 
            //interpolation, to maximize the quality of the scaled image
            newBmpGraphics.InterpolationMode = interpolationMode; //  InterpolationMode.HighQualityBicubic;

            newBmpGraphics.ScaleTransform((float)xScaleFactor, (float)yScaleFactor);

            //Draw the bitmap in the graphics object, which will apply
            //the scale transform
            //Note that pixel units must be specified to ensure the framework doesn't attempt
            //to compensate for varying horizontal resolutions in images by resizing; in this case,
            //that's the opposite of what we want.
            Rectangle drawRect = new Rectangle(0, 0, inputBmp.Size.Width, inputBmp.Size.Height);
            newBmpGraphics.DrawImage(inputBmp, drawRect, drawRect, GraphicsUnit.Pixel);

            //Return the bitmap, as the operations on the graphics object
            //are applied to the bitmap
            newBmpGraphics.Dispose();

            //newBmp will have a RawFormat of MemoryBmp because it was created
            //from scratch instead of being based on inputBmp.  Since it it inconvenient
            //for the returned version of a bitmap to be of a different format, now convert
            //the scaled bitmap to the format of the source bitmap
            return ConvertBitmap(newBmp, inputBmp.RawFormat);
        }

        /// <summary>
        /// Resizes a bitmap's width and height independently
        /// </summary>
        /// <param name="inputBmp">Bitmap to resize</param>
        /// <param name="imgWidth">New width</param>
        /// <param name="imgHeight">New height</param>
        /// <returns>Resized bitmap</returns>
        public static Bitmap ResizeBitmap(Bitmap inputBmp, int imgWidth, int imgHeight, InterpolationMode interpolationMode = InterpolationMode.HighQualityBicubic, PixelFormat pixelFormat = PixelFormat.Format32bppArgb)
        {
            //Simply compute scale factors that result in the desired size, then call ScaleBitmap
            return ScaleBitmap(inputBmp,
                               (float)imgWidth / (float)inputBmp.Size.Width,
                               (float)imgHeight / (float)inputBmp.Size.Height, interpolationMode, pixelFormat);
        }

        /// <summary>
        /// Generates a thumbnail of the bitmap.  This is effectively a specialized
        /// resize function, which maintains the aspect ratio of the image while
        /// resizing it to ensure that both its width and height are within
        /// caller-specified maximums
        /// </summary>
        /// <param name="inputBmp">Bitmap for which to generate thumbnail</param>
        /// <param name="maxWidth">Maximum width of thumbnail</param>
        /// <param name="maxHeight">Maximum height of thumbnail</param>
        /// <returns>Thumbnail of inputBmp w/ the same aspect ratio, but
        /// width and height both less than or equal to the maximum limits</returns>
        public static Bitmap ThumbnailBitmap(Bitmap inputBmp, int maxWidth, int maxHeight)
        {
            //Compute the scaling factor that will scale the bitmap witdh
            //to the max width, and the other scaling factor that will scale
            //the bitmap height to the max height.
            //Apply the lower of the two, then if the other dimension is still
            //outside the caller-defined limits, compute the scaling factor
            //which will bring that dimension within the limits.
            double widthScaleFactor = (double)maxWidth / (double)inputBmp.Size.Width;
            double heightScaleFactor = (double)maxHeight / (double)inputBmp.Size.Height;
            double finalScaleFactor = 0;

            //Now pick the smaller scale factor
            if (widthScaleFactor < heightScaleFactor)
            {
                //If this scale factor doesn't bring the height
                //within the required maximum, combine this width
                //scale factor with an additional scaling factor
                //to take the height the rest of the way down
                if ((double)inputBmp.Size.Height * widthScaleFactor > maxHeight)
                {
                    //Need to scale height further
                    heightScaleFactor = (double)(maxHeight * widthScaleFactor) / (double)inputBmp.Size.Height;

                    finalScaleFactor = widthScaleFactor * heightScaleFactor;
                }
                else
                {
                    //Width scale factor brings both dimensions inline sufficiently
                    finalScaleFactor = widthScaleFactor;
                }
            }
            else
            {
                //Else, height scale factor is smaller than width.
                //Apply the same logic as above, but with the roles of the width
                //and height scale factors reversed
                if ((double)inputBmp.Size.Width * heightScaleFactor > maxWidth)
                {
                    //Need to scale height further
                    widthScaleFactor = (double)(maxWidth * heightScaleFactor) / (double)inputBmp.Size.Width;

                    finalScaleFactor = widthScaleFactor * heightScaleFactor;
                }
                else
                {
                    //Height scale factor brings both dimensions inline sufficiently
                    finalScaleFactor = heightScaleFactor;
                }
            }

            return ScaleBitmap(inputBmp, finalScaleFactor);
        }

        /// <summary>
        /// Method to rotate an Image object. The result can be one of three cases:
        /// - upsizeOk = true: output image will be larger than the input, and no clipping occurs 
        /// - upsizeOk = false & clipOk = true: output same size as input, clipping occurs
        /// - upsizeOk = false & clipOk = false: output same size as input, image reduced, no clipping
        /// 
        /// A background color must be specified, and this color will fill the edges that are not 
        /// occupied by the rotated image. If color = transparent the output image will be 32-bit, 
        /// otherwise the output image will be 24-bit.
        /// 
        /// Note that this method always returns a new Bitmap object, even if rotation is zero - in 
        /// which case the returned object is a clone of the input object. 
        /// </summary>
        /// <param name="inputImage">input Image object, is not modified</param>
        /// <param name="angleDegrees">angle of rotation, in degrees</param>
        /// <param name="upsizeOk">see comments above</param>
        /// <param name="clipOk">see comments above, not used if upsizeOk = true</param>
        /// <param name="backgroundColor">color to fill exposed parts of the background</param>
        /// <returns>new Bitmap object, may be larger than input image</returns>
        public static Bitmap RotateImage(Image inputImage, float angleDegrees, bool upsizeOk,
                                         bool clipOk, Color backgroundColor)
        {
            // Test for zero rotation and return a clone of the input image
            if (angleDegrees == 0f)
                return (Bitmap)inputImage.Clone();

            // Set up old and new image dimensions, assuming upsizing not wanted and clipping OK
            int oldWidth = inputImage.Width;
            int oldHeight = inputImage.Height;
            int newWidth = oldWidth;
            int newHeight = oldHeight;
            float scaleFactor = 1f;

            // If upsizing wanted or clipping not OK calculate the size of the resulting bitmap
            if (upsizeOk || !clipOk)
            {
                double angleRadians = angleDegrees * Math.PI / 180d;

                double cos = Math.Abs(Math.Cos(angleRadians));
                double sin = Math.Abs(Math.Sin(angleRadians));
                newWidth = (int)Math.Round(oldWidth * cos + oldHeight * sin);
                newHeight = (int)Math.Round(oldWidth * sin + oldHeight * cos);
            }

            // If upsizing not wanted and clipping not OK need a scaling factor
            if (!upsizeOk && !clipOk)
            {
                scaleFactor = Math.Min((float)oldWidth / newWidth, (float)oldHeight / newHeight);
                newWidth = oldWidth;
                newHeight = oldHeight;
            }

            // Create the new bitmap object. If background color is transparent it must be 32-bit, 
            //  otherwise 24-bit is good enough.
            Bitmap newBitmap = new Bitmap(newWidth, newHeight, backgroundColor == Color.Transparent ?
                                             PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb);
            newBitmap.SetResolution(inputImage.HorizontalResolution, inputImage.VerticalResolution);

            // Create the Graphics object that does the work
            using (Graphics graphicsObject = Graphics.FromImage(newBitmap))
            {
                graphicsObject.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsObject.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphicsObject.SmoothingMode = SmoothingMode.HighQuality;

                // Fill in the specified background color if necessary
                if (backgroundColor != Color.Transparent)
                    graphicsObject.Clear(backgroundColor);

                // Set up the built-in transformation matrix to do the rotation and maybe scaling
                graphicsObject.TranslateTransform(newWidth / 2f, newHeight / 2f);

                if (scaleFactor != 1f)
                    graphicsObject.ScaleTransform(scaleFactor, scaleFactor);

                graphicsObject.RotateTransform(angleDegrees);
                graphicsObject.TranslateTransform(-oldWidth / 2f, -oldHeight / 2f);

                // Draw the result 
                graphicsObject.DrawImage(inputImage, 0, 0);
            }

            return newBitmap;
        }

        /// <summary>
        /// method to rotate an image either clockwise or counter-clockwise - NOTE: Image can be clipped
        /// </summary>
        /// <param name="img">the image to be rotated</param>
        /// <param name="rotationAngle">the angle (in degrees).
        /// NOTE: 
        /// Positive values will rotate clockwise
        /// negative values will rotate counter-clockwise
        /// </param>
        /// <returns></returns>
        public static Image RotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }

        public static Bitmap MirrorYBitmap(Bitmap inputBmp) // RotateNoneFlipY
        {
            //Copy bitmap
            Bitmap newBmp = (Bitmap)inputBmp.Clone();

            newBmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

            //The RotateFlip transformation converts bitmaps to memoryBmp,
            //which is uncool.  Convert back now
            return ConvertBitmap(newBmp, inputBmp.RawFormat);
        }

        public static Bitmap RotateFlipBitmap(Bitmap inputBmp, RotateFlipType flipType) // RotateNoneFlipY
        {
            //Copy bitmap
            Bitmap newBmp = (Bitmap)inputBmp.Clone();

            newBmp.RotateFlip(flipType);

            //The RotateFlip transformation converts bitmaps to memoryBmp,
            //which is uncool.  Convert back now
            return ConvertBitmap(newBmp, inputBmp.RawFormat);
        }

        public static Bitmap RotateBitmapRight90(Bitmap inputBmp)
        {
            //Copy bitmap
            Bitmap newBmp = (Bitmap)inputBmp.Clone();

            newBmp.RotateFlip(RotateFlipType.Rotate90FlipNone);

            //The RotateFlip transformation converts bitmaps to memoryBmp,
            //which is uncool.  Convert back now
            return ConvertBitmap(newBmp, inputBmp.RawFormat);
        }

        public static Bitmap RotateBitmapRight180(Bitmap inputBmp)
        {
            //Copy bitmap
            Bitmap newBmp = (Bitmap)inputBmp.Clone();

            newBmp.RotateFlip(RotateFlipType.Rotate180FlipNone);


            //The RotateFlip transformation converts bitmaps to memoryBmp,
            //which is uncool.  Convert back now
            return ConvertBitmap(newBmp, inputBmp.RawFormat);
        }

        public static Bitmap RotateBitmapRight270(Bitmap inputBmp)
        {
            //Copy bitmap
            Bitmap newBmp = (Bitmap)inputBmp.Clone();

            newBmp.RotateFlip(RotateFlipType.Rotate270FlipNone);


            //The RotateFlip transformation converts bitmaps to memoryBmp,
            //which is uncool.  Convert back now
            return ConvertBitmap(newBmp, inputBmp.RawFormat);
        }

        /// <summary>
        /// Reverses a bitmap, effectively rotating it 180 degrees in 3D space about
        /// the Y axis.  Results in a "mirror image" of the bitmap, reversed much
        /// as it would be in a mirror
        /// </summary>
        /// <param name="inputBmp"></param>
        /// <returns></returns>
        public static Bitmap ReverseBitmap(Bitmap inputBmp)
        {
            //Copy the bitmap to a new bitmap object
            Bitmap newBmp = (Bitmap)inputBmp.Clone();

            //Flip the bitmap
            newBmp.RotateFlip(RotateFlipType.RotateNoneFlipX);


            //The RotateFlip transformation converts bitmaps to memoryBmp,
            //which is uncool.  Convert back now
            return ConvertBitmap(newBmp, inputBmp.RawFormat);
        }

        /// <summary>
        /// Reverses a bitmap, effectively rotating it 180 degrees in 3D space about
        /// the X axis.  Results in an upside-down view of the image
        /// </summary>
        /// <param name="inputBmp"></param>
        /// <returns></returns>
        public static Bitmap FlipBitmap(Bitmap inputBmp)
        {
            //Copy the bitmap to a new bitmap object
            Bitmap newBmp = (Bitmap)inputBmp.Clone();

            //Flip the bitmap
            newBmp.RotateFlip(RotateFlipType.RotateNoneFlipY);


            //The RotateFlip transformation converts bitmaps to memoryBmp,
            //which is uncool.  Convert back now
            return ConvertBitmap(newBmp, inputBmp.RawFormat);
        }

        /// <summary>
        /// Renders a bitmap over another bitmap, with a specific alpha value.
        /// This can be used to overlay a logo or watermark over a bitmap
        /// </summary>
        /// <param name="destBmp">Bitmap over which image is to be overlaid</param>
        /// <param name="bmpToOverlay">Bitmap to overlay</param>
        /// <param name="overlayAlpha">Alpha value fo overlay bitmap.  0 = fully transparent, 100 = fully opaque</param>
        /// <param name="overlayPoint">Location in destination bitmap where overlay image will be placed</param>
        /// <returns></returns>
        public static Bitmap OverlayBitmap(Bitmap destBmp, Bitmap bmpToOverlay, int overlayAlpha, Point overlayPoint)
        {
            //Convert alpha to a 0..1 scale
            float overlayAlphaFloat = (float)overlayAlpha / 100.0f;

            //Copy the destination bitmap
            //NOTE: Can't clone here, because if destBmp is indexed instead of just RGB, 
            //Graphics.FromImage will fail
            Bitmap newBmp = new Bitmap(destBmp.Size.Width,
                                       destBmp.Size.Height);

            //Create a graphics object attached to the bitmap
            Graphics newBmpGraphics = Graphics.FromImage(newBmp);

            //Draw the input bitmap into this new graphics object
            newBmpGraphics.DrawImage(destBmp,
                                     new Rectangle(0, 0,
                                                   destBmp.Size.Width,
                                                   destBmp.Size.Height),
                                     0, 0, destBmp.Size.Width, destBmp.Size.Height,
                                     GraphicsUnit.Pixel);

            //Create a new bitmap object the same size as the overlay bitmap
            Bitmap overlayBmp = new Bitmap(bmpToOverlay.Size.Width, bmpToOverlay.Size.Height);

            //Make overlayBmp transparent
            overlayBmp.MakeTransparent(overlayBmp.GetPixel(0, 0));

            //Create a graphics object attached to the bitmap
            Graphics overlayBmpGraphics = Graphics.FromImage(overlayBmp);

            //Create a color matrix which will be applied to the overlay bitmap
            //to modify the alpha of the entire image
            float[][] colorMatrixItems = {
				new float[] {1, 0, 0, 0, 0},
					new float[] {0, 1, 0, 0, 0},
					new float[] {0, 0, 1, 0, 0},
					new float[] {0, 0, 0, overlayAlphaFloat, 0}, 
					new float[] {0, 0, 0, 0, 1}
			};

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixItems);

            //Create an ImageAttributes class to contain a color matrix attribute
            ImageAttributes imageAttrs = new ImageAttributes();
            imageAttrs.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            //Draw the overlay bitmap into the graphics object, applying the image attributes
            //which includes the reduced alpha
            Rectangle drawRect = new Rectangle(0, 0, bmpToOverlay.Size.Width, bmpToOverlay.Size.Height);
            overlayBmpGraphics.DrawImage(bmpToOverlay,
                                         drawRect,
                                         0, 0, bmpToOverlay.Size.Width, bmpToOverlay.Size.Height,
                                         GraphicsUnit.Pixel,
                                         imageAttrs);
            overlayBmpGraphics.Dispose();

            //overlayBmp now contains bmpToOverlay w/ the alpha applied.
            //Draw it onto the target graphics object
            //Note that pixel units must be specified to ensure the framework doesn't attempt
            //to compensate for varying horizontal resolutions in images by resizing; in this case,
            //that's the opposite of what we want.
            newBmpGraphics.DrawImage(overlayBmp,
                                     new Rectangle(overlayPoint.X, overlayPoint.Y, bmpToOverlay.Width, bmpToOverlay.Height),
                                     drawRect,
                                     GraphicsUnit.Pixel);

            newBmpGraphics.Dispose();

            //Recall that newBmp was created as a memory bitmap; convert it to the format
            //of the input bitmap
            return ConvertBitmap(newBmp, destBmp.RawFormat); ;
        }

        /// <summary>
        /// Renders a bitmap over another bitmap, with a specific alpha value.
        /// This can be used to overlay a logo or watermark over a bitmap
        /// </summary>
        /// <param name="destBmp">Bitmap over which image is to be overlaid</param>
        /// <param name="bmpToOverlay">Bitmap to overlay</param>
        /// <param name="overlayAlpha">Alpha value fo overlay bitmap.  0 = fully transparent, 100 = fully opaque</param>
        /// <param name="corner">Corner of destination bitmap to place overlay bitmap</param>
        /// <returns></returns>
        public static Bitmap OverlayBitmap(Bitmap destBmp, Bitmap bmpToOverlay, int overlayAlpha, ImageCornerEnum corner)
        {
            //Translate corner to rectangle and pass through to other impl
            Point overlayPoint;

            if (corner.Equals(ImageCornerEnum.TopLeft))
            {
                overlayPoint = new Point(0, 0);
            }
            else if (corner.Equals(ImageCornerEnum.TopRight))
            {
                overlayPoint = new Point(destBmp.Size.Width - bmpToOverlay.Size.Width, 0);
            }
            else if (corner.Equals(ImageCornerEnum.BottomRight))
            {
                overlayPoint = new Point(destBmp.Size.Width - bmpToOverlay.Size.Width,
                                         destBmp.Size.Height - bmpToOverlay.Size.Height);
            }
            else if (corner.Equals(ImageCornerEnum.Center))
            {
                overlayPoint = new Point(destBmp.Size.Width / 2 - bmpToOverlay.Size.Width / 2,
                                         destBmp.Size.Height / 2 - bmpToOverlay.Size.Height / 2);
            }
            else
            {
                overlayPoint = new Point(0,
                                         destBmp.Size.Height - bmpToOverlay.Size.Height);
            }

            return OverlayBitmap(destBmp, bmpToOverlay, overlayAlpha, overlayPoint);
        }

        public static Bitmap OverlayBitmap(Bitmap destBmp, Bitmap bmpToOverlay, Point overlayPoint)
        {
            return OverlayBitmap(destBmp, bmpToOverlay, 0, overlayPoint);
        }
        public static Bitmap OverlayBitmap(Bitmap destBmp, Bitmap bmpToOverlay, ImageCornerEnum corner)
        {
            return OverlayBitmap(destBmp, bmpToOverlay, 0, corner);
        }

        /// <summary>
        /// Crops an image, resulting in a new image consisting of the portion of the
        /// original image contained in a provided bounding rectangle
        /// </summary>
        /// <param name="inputBmp">Bitmap to crop</param>
        /// <param name="cropRectangle">SinglePlexThreshold_DuplexRectangle specifying the range of pixels
        /// within the image which is to be retained</param>
        /// <returns>New bitmap consisting of the contents of the crop rectangle</returns>
        public static Bitmap CropBitmap(Bitmap inputBmp, Rectangle cropRectangle)
        {
            //Create a new bitmap object based on the input
            Bitmap newBmp = new Bitmap(cropRectangle.Width,
                                       cropRectangle.Height,
                                       PixelFormat.Format24bppRgb);//Graphics.FromImage doesn't like Indexed pixel format

            //Create a graphics object and attach it to the bitmap
            Graphics newBmpGraphics = Graphics.FromImage(newBmp);

            //Draw the portion of the input image in the crop rectangle
            //in the graphics object
            newBmpGraphics.DrawImage(inputBmp,
                                     new Rectangle(0, 0, cropRectangle.Width, cropRectangle.Height),
                                     cropRectangle,
                                     GraphicsUnit.Pixel);

            //Return the bitmap
            newBmpGraphics.Dispose();

            //newBmp will have a RawFormat of MemoryBmp because it was created
            //from scratch instead of being based on inputBmp.  Since it it inconvenient
            //for the returned version of a bitmap to be of a different format, now convert
            //the scaled bitmap to the format of the source bitmap
            return ConvertBitmap(newBmp, inputBmp.RawFormat);
        }

        /// <summary>
        /// Crops an image, resulting in a new image consisting of the portion of the
        /// original image contained in a provided bounding rectangle
        /// </summary>
        /// <param name="inputBmp">Bitmap to crop</param>
        /// <param name="cropRectangle">SinglePlexThreshold_DuplexRectangle specifying the range of pixels
        /// within the image which is to be retained</param>
        /// <returns>New bitmap consisting of the contents of the crop rectangle</returns>
        public static Bitmap CropBitmap(Bitmap inputBmp, Point startLocation, Rectangle cropRectangle)
        {
            //Create a new bitmap object based on the input
            Bitmap newBmp = new Bitmap(cropRectangle.Width,
                                       cropRectangle.Height,
                                       PixelFormat.Format24bppRgb);//Graphics.FromImage doesn't like Indexed pixel format

            //Create a graphics object and attach it to the bitmap
            Graphics newBmpGraphics = Graphics.FromImage(newBmp);

            //Draw the portion of the input image in the crop rectangle
            //in the graphics object
            newBmpGraphics.DrawImage(inputBmp,
                                     cropRectangle,
                                     new Rectangle(startLocation.X, startLocation.Y, cropRectangle.Width, cropRectangle.Height),
                                     GraphicsUnit.Pixel);

            //Return the bitmap
            newBmpGraphics.Dispose();

            //newBmp will have a RawFormat of MemoryBmp because it was created
            //from scratch instead of being based on inputBmp.  Since it it inconvenient
            //for the returned version of a bitmap to be of a different format, now convert
            //the scaled bitmap to the format of the source bitmap
            return ConvertBitmap(newBmp, inputBmp.RawFormat);
        }

        public static String MimeTypeFromImageFormat(ImageFormat format)
        {
            if (format.Equals(ImageFormat.Jpeg))
            {
                return MIME_JPEG;
            }
            else if (format.Equals(ImageFormat.Gif))
            {
                return MIME_GIF;
            }
            else if (format.Equals(ImageFormat.Bmp))
            {
                return MIME_BMP;
            }
            else if (format.Equals(ImageFormat.Tiff))
            {
                return MIME_TIFF;
            }
            else if (format.Equals(ImageFormat.Png))
            {
                return MIME_PNG;
            }
            else
            {
                throw new ArgumentException("Unsupported  image format '" + format + "'", "format");
            }
        }

        public static ImageFormat ImageFormatFromMimeType(String mimeType)
        {
            switch (mimeType)
            {
                case MIME_JPEG:
                case MIME_PJPEG:
                    return ImageFormat.Jpeg;

                case MIME_GIF:
                    return ImageFormat.Gif;

                case MIME_BMP:
                    return ImageFormat.Bmp;

                case MIME_TIFF:
                    return ImageFormat.Tiff;

                case MIME_PNG:
                    return ImageFormat.Png;

                default:
                    throw new ArgumentException("Unsupported  MIME type '" + mimeType + "'", "mimeType");
            }
        }

        private static ImageCodecInfo FindCodecForType(String mimeType)
        {
            ImageCodecInfo[] imgEncoders = ImageCodecInfo.GetImageEncoders();

            for (int i = 0; i < imgEncoders.GetLength(0); i++)
            {
                if (imgEncoders[i].MimeType == mimeType)
                {
                    //Found it
                    return imgEncoders[i];
                }
            }

            //No encoders match
            return null;
        }

        public static Bitmap capture(Control window, Rectangle rc)
        {
            if (window == null)
                return null;

            if (window.Visible == false)
                window.Visible = true;

            if (!window.IsHandleCreated)
                window.CreateControl();

            Bitmap memoryImage = null;

            // Create new graphics object using handle to window.
            using (Graphics graphics = window.CreateGraphics())
            {
                memoryImage = new Bitmap(rc.Width,
                              rc.Height, graphics);

                using (Graphics memoryGrahics =
                        Graphics.FromImage(memoryImage))
                {
                    //memoryGrahics.CopyFromScreen(rc.X, rc.Y,
                    //   location.X, location.Y, rc.Size, CopyPixelOperation.SourceCopy);
                    memoryGrahics.CopyFromScreen(rc.X, rc.Y, 0, 0, rc.Size, CopyPixelOperation.SourceCopy);
                }
            }
            return memoryImage;
        }

        public static Bitmap ByteArrayToBitmap(byte[] pixels, int width, int height, PixelFormat format)
        {
            if (pixels == null)
                return null;

            var bitmap = new Bitmap(width, height, format);

            var data = bitmap.LockBits(
                new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite,
                bitmap.PixelFormat);

            Marshal.Copy(pixels, 0, data.Scan0, pixels.Length);

            bitmap.UnlockBits(data);

            return bitmap;
        }

        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {

            BitmapData bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
            int numbytes = bmpdata.Stride * bitmap.Height;
            byte[] bytedata = new byte[numbytes];
            IntPtr ptr = bmpdata.Scan0;

            Marshal.Copy(ptr, bytedata, 0, numbytes);

            bitmap.UnlockBits(bmpdata);

            return bytedata;

        }
        public static byte[] ConvertBitmapToByteArray(Bitmap img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static int[] ConvertBitmapToIntArray(Bitmap img)
        {
            ImageConverter converter = new ImageConverter();
            return (int[])converter.ConvertTo(img, typeof(int[]));
        }

        public static int[,] Convert1DArrayTo2DArray(int[] src, int desiredWidth, int desiredHeight)
        {
            int[,] dest = new int[desiredWidth, desiredHeight];

            int count = src.Length;
            if (desiredWidth * desiredHeight < count)
            {
                count = desiredWidth * desiredHeight;
            }

            int index = 0;
            for (int y = 0; y < desiredHeight; y++)
            {
                for (int x = 0; x < desiredWidth; x++)
                {
                    dest[x, y] = src[index++];
                    if (index == count)
                    {
                        return dest;
                    }
                }
            }
            return dest;
        }

        public static int[] Convert2DArrayTo1DArray(int[,] src, int desiredWidth, int desiredHeight) 
        {
            int[] dest = new int[desiredWidth * desiredHeight];

            int count = src.Length;
            if (desiredWidth * desiredHeight < count)
            {
                count = desiredWidth * desiredHeight;
            }

            int index = 0;
            for (int y = 0; y < desiredHeight; y++)
            {
                for (int x = 0; x < desiredWidth; x++)
                {
                    dest[index++] = src[x, y];
                    if (index == count)
                    {
                        return dest;
                    }
                }
            }
            return dest;
        }

        public static byte[] FauxColorRGB(double val, double min, double max)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;
            val = (val - min) / (max - min);
            if (val <= 0.2)
            {
                b = (byte)((val / 0.2) * 255);
            }
            else if (val > 0.2 && val <= 0.7)
            {
                b = (byte)((1.0 - ((val - 0.2) / 0.5)) * 255);
            }
            if (val >= 0.2 && val <= 0.6)
            {
                g = (byte)(((val - 0.2) / 0.4) * 255);
            }
            else if (val > 0.6 && val <= 0.9)
            {
                g = (byte)((1.0 - ((val - 0.6) / 0.3)) * 255);
            }
            if (val >= 0.5)
            {
                r = (byte)(((val - 0.5) / 0.5) * 255);
            }
            return new byte[] { r, g, b };
        }

        public static byte[] Greyscale(double val, double min, double max)
        {
            byte y = 0;
            val = (val - min) / (max - min);
            y = (byte)((1.0 - val) * 255);
            return new byte[] { y, y, y };
        }

        #region Visible Spectrum HeatMap
        public static class HeatSpectrumToColor
        {
            // Thanks to Ian Boyd's excellent post here:
            // http://stackoverflow.com/questions/2374959/algorithm-to-convert-any-positive-integer-to-an-rgb-value

            private const double MinVisibleWaveLength = 450.0;
            private const double MaxVisibleWaveLength = 700.0;
            private const double Gamma = 0.8;

            private const int IntensityMax = 255;
            public static System.Drawing.Color HeatToColor(double value, double MinValue, double MaxValues)
            {
                double wavelength = 0;
                double Red = 0;
                double Green = 0;
                double Blue = 0;
                double Factor = 0;
                double scaled = 0;

                scaled = (value - MinValue) / (MaxValues - MinValue);

                wavelength = scaled * (MaxVisibleWaveLength - MinVisibleWaveLength) + MinVisibleWaveLength;

                double wavelengthFloor = Math.Floor(wavelength);
                if (wavelengthFloor >= 380 && wavelengthFloor < 440)
                {
                    //case 380: // TODO: to 439
                    Red = -(wavelength - 440) / (440 - 380);
                    Green = 0.0;
                    Blue = 1.0;
                }
                else if (wavelengthFloor >= 440 && wavelengthFloor < 490)
                {
                    //case 440: // TODO: to 489
                    Red = 0.0;
                    Green = (wavelength - 440) / (490 - 440);
                    Blue = 1.0;
                }
                else if (wavelengthFloor >= 490 && wavelengthFloor < 510)
                {
                    //case 490: // TODO: to 509
                    Red = 0.0;
                    Green = 1.0;
                    Blue = -(wavelength - 510) / (510 - 490);
                }
                else if (wavelengthFloor >= 510 && wavelengthFloor < 580)
                {
                    //case 510: // TODO: to 579
                    Red = (wavelength - 510) / (580 - 510);
                    Green = 1.0;
                    Blue = 0.0;
                }
                else if (wavelengthFloor >= 580 && wavelengthFloor < 645)
                {
                    //case 580: // TODO: to 644
                    Red = 1.0;
                    Green = -(wavelength - 645) / (645 - 580);
                    Blue = 0.0;
                }
                else if (wavelengthFloor >= 645 && wavelengthFloor < 780)
                {
                    //case 645: // TODO: to 780
                    Red = 1.0;
                    Green = 0.0;
                    Blue = 0.0;
                }
                else
                {
                    //default:
                    Red = 0.0;
                    Green = 0.0;
                    Blue = 0.0;
                }

                // Let the intensity fall off near the vision limits
                if (wavelengthFloor >= 380 && wavelengthFloor < 419)
                {
                    //case 380: // TODO: to 419
                    Factor = 0.3 + 0.7 * (wavelength - 380) / (420 - 380);
                }
                else if (wavelengthFloor >= 420 && wavelengthFloor < 700)
                {
                    //case 420: // TODO: to 700
                    Factor = 1.0;
                }
                else if (wavelengthFloor >= 701 && wavelengthFloor < 780)
                {
                    //case 701: // TODO: to 780
                    Factor = 0.3 + 0.7 * (780 - wavelength) / (780 - 700);
                }
                else
                {
                    //default:
                    Factor = 0.0;
                }

                int R = Adjust(Red, Factor);
                int G = Adjust(Green, Factor);
                int B = Adjust(Blue, Factor);

                Color result = System.Drawing.Color.FromArgb(255, R, G, B);
                RGBHSV.HSV resulthsv = new RGBHSV.HSV();
                resulthsv = RGBHSV.ColorToHSV(result);
                resulthsv.Value = 0.7 + 0.1 * scaled + 0.2 * Math.Sin(scaled * Math.PI);

                result = RGBHSV.HSVToColor(resulthsv);

                return result;
            }

            private static int Adjust(double Color, double Factor)
            {
                if (Color == 0)
                {
                    return 0;
                }
                else
                {
                    return (int)Math.Round(IntensityMax * Math.Pow(Color * Factor, Gamma));
                }
            }
        }

        public static class RGBHSV
        {
            public class HSV
            {
                public HSV()
                {
                    Hue = 0;
                    Saturation = 0;
                    Value = 0;
                }
                public HSV(double H, double S, double V)
                {
                    Hue = H;
                    Saturation = S;
                    Value = V;
                }
                public double Hue;
                public double Saturation;
                public double Value;
            }

            public static HSV ColorToHSV(Color color)
            {
                int max = Math.Max(color.R, Math.Max(color.G, color.B));
                int min = Math.Min(color.R, Math.Min(color.G, color.B));
                HSV result = new HSV();
                {
                    result.Hue = color.GetHue();
                    result.Saturation = (max == 0) ? 0 : 1.0 - (1.0 * min / max);
                    result.Value = max / 255.0;
                }
                return result;
            }

            public static Color HSVToColor(HSV hsv)
            {
                int hi = 0;
                double f = 0;

                {
                    hi = Convert.ToInt32(Math.Floor(hsv.Hue / 60)) % 6;
                    f = hsv.Hue / 60 - Math.Floor(hsv.Hue / 60);
                    hsv.Value = hsv.Value * 255;
                    int v = Convert.ToInt32(hsv.Value);
                    int p = Convert.ToInt32(hsv.Value * (1 - hsv.Saturation));
                    int q = Convert.ToInt32(hsv.Value * (1 - f * hsv.Saturation));
                    int t = Convert.ToInt32(hsv.Value * (1 - (1 - f) * hsv.Saturation));

                    if (hi == 0)
                    {
                        return Color.FromArgb(255, v, t, p);
                    }
                    else if (hi == 1)
                    {
                        return Color.FromArgb(255, q, v, p);
                    }
                    else if (hi == 2)
                    {
                        return Color.FromArgb(255, p, v, t);
                    }
                    else if (hi == 3)
                    {
                        return Color.FromArgb(255, p, q, v);
                    }
                    else if (hi == 4)
                    {
                        return Color.FromArgb(255, t, p, v);
                    }
                    else
                    {
                        return Color.FromArgb(255, v, p, q);
                    }
                }
            }
        }
        #endregion Visible Spectrum HeatMap

        // value between 0 and 1 (percent)   
        public static Color GetHeatMapColor(double value)
        {
            byte R = 0;
            byte G = 0;
            byte B = 0;

            // y = mx + b
            // m = 4
            // x = value
            // y = RGB color
            if (0 <= value && value <= 0.125)
            {
                R = 0;
                G = 0;
                B = (byte)(255 * (4 * value + .5)); // .5 - 1 // b = 1/2
            }
            else if (0.125 < value && value <= .375)
            {
                R = 0;
                G = (byte)(255 * (4 * value - .5)); // 0 - 1 // b = - 1/2
                B = 0;
            }
            else if (.375 < value && value <= .625)
            {
                R = (byte)(255 * (4 * value - 1.5)); // 0 - 1 // b = - 3/2
                G = 255;
                B = (byte)(255 * (-4 * value + 2.5)); // 1 - 0 // b = 5/2
            }
            else if (.625 < value && value <= .875)
            {
                R = 255;
                G = (byte)(255 * (-4 * value + 3.5)); // 1 - 0 // b = 7/2
                B = 0;
            }
            else if (.875 < value && value <= 1)
            {
                R = (byte)(255 * (-4 * value + 4.5)); // 1 - .5 // b = 9/2
                G = 0;
                B = 0;
            }
            else
            {    // should never happen - value > 1
                R = 0;
                G = 0;
                B = 0;
            }

            return Color.FromArgb(R, G, B);
        }

        // value between 0 and 1 (percent)   
        public static Color GetHeatMapColor(double value, double min, double max, bool logScale)
        {
            double range = Math.Abs(max - min);
            double val = value - min;
            if (logScale == true) // Scale to values 0 - 1 after applying log scale 
            {
                if (val >= 1 && range >= 1)
                {
                    value = Math.Log(val) / Math.Log(range);
                }
                else
                {
                    value = 0.0;
                }
            }
            else
            {
                value = val/range;
            }

            return GetHeatMapColor(value);
        }

        public static Bitmap ConvertImageToHeatmapBitMap(Bitmap img, int[,] Heatmap, bool logScale) // cmap is the desired color spectrum to use
        {
            return Convert2DIntArrayToHeatmapBitMap(ConvertBitmapTo2DIntArray(img), Heatmap, logScale);
        }

        public static Bitmap ConvertImageToHeatmapBitMap(Bitmap img, bool logScale) // cmap is the desired color spectrum to use
        {
            int[,] Heatmap = new int[64, 4]; // Heatmap
            ColorMapExt cmap = new ColorMapExt();
            Heatmap = cmap.Heatmap();

            return Convert2DIntArrayToHeatmapBitMap(ConvertBitmapTo2DIntArray(img), Heatmap, logScale);
        }

        public static Bitmap Convert2DIntArrayToHeatmapBitMap(int[,] data, bool logScale) // cmap is the desired color spectrum to use
        {
            int[,] Heatmap = new int[64, 4]; // Heatmap
            ColorMapExt cmap = new ColorMapExt();
            Heatmap = cmap.Heatmap();

            return Convert2DIntArrayToHeatmapBitMap(data, Heatmap, 0, 255, logScale);
        }

        public static Bitmap Convert2DIntArrayToHeatmapBitMap(int[,] data, int[,] cmap, double min, double max, bool logScale) // cmap is the desired color spectrum to use
        {
            int width = data.GetLength(0);
            int height = data.GetLength(1);

            Bitmap bitmap = new Bitmap(width, height);

            try
            {
                // Already locked!
                //LockBitmap lockBitmap = new LockBitmap(img);
                //lockBitmap.LockBits();

                //double range = Math.Abs(max - min);
                //double value;
                //byte r, g, b = 0;
                int k;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        k = height - j - 1;
                        bitmap.SetPixel(i, k, GetHeatMapColor(data[i, j], min, max, logScale));
                        // bitmap.SetPixel(i, k, HeatSpectrumToColor.HeatToColor(data[i, j], min, max)); // linear scale

                        //value = (data[i, j] - min) / range;
                        //if (logScale == true) // Scale to values 0 - 1 after applying log scale 
                        //{
                        //    if (value >= 1 && range >= 1)
                        //    {
                        //        value = Math.Log(value) / Math.Log(range);
                        //    }
                        //    else
                        //    {
                        //        value = 0.0;
                        //    }
                        //}
                        //else
                        //{
                        //    value /= range;
                        //}

                        //bitmap.SetPixel(i, k, HeatSpectrumToColor.HeatToColor(value, 0, 1.0)); // linear or log scale
                        ////bitmap.SetPixel(i, k, Color.FromArgb(255, r, g, b));
                    }
                }

                //lockBitmap.UnlockBits();
            }
            catch (Exception ex)
            {
                string exMessage = ex.Message;
            }

            return RotateFlipBitmap(bitmap, RotateFlipType.RotateNoneFlipY);
        }

        public static Bitmap Convert2DIntArrayToHeatmapBitMap(int[,] data, int[,] cmap, bool logScale) // cmap is the desired color spectrum to use
        {
            int width = data.GetLength(0);
            int height = data.GetLength(1);

            Bitmap bitmap = new Bitmap(width, height);
            try
            {
                // Already locked!
                //LockBitmap lockBitmap = new LockBitmap(img);
                //lockBitmap.LockBits();

                double min = 100000000;
                double max = 0;
                // Get min/max of the data
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (min > 0)
                            min = Math.Min(min, data[i, j]);
                        max = Math.Max(max, data[i, j]);
                    }
                }

                //double range = Math.Abs(max - min);
                //double value;
                //byte r, g, b = 0;
                int k;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        k = height - j - 1;
                        bitmap.SetPixel(i, k, GetHeatMapColor(data[i, j], min, max, logScale));
                        // bitmap.SetPixel(i, k, HeatSpectrumToColor.HeatToColor(data[i, j], min, max)); // linear scale

                        //value = (data[i, j] - min) / range;
                        //if (logScale == true) // Scale to values 0 - 1 after applying log scale 
                        //{
                        //    if (value >= 1 && range >= 1)
                        //    {
                        //        value = Math.Log(value) / Math.Log(range);
                        //    }
                        //    else
                        //    {
                        //        value = 0.0;
                        //    }
                        //}
                        //else
                        //{
                        //    value /= range;
                        //}

                        //bitmap.SetPixel(i, k, HeatSpectrumToColor.HeatToColor(value, 0, 1.0)); // linear or log scale
                        ////bitmap.SetPixel(i, k, Color.FromArgb(255, r, g, b));
                    }
                }

                //lockBitmap.UnlockBits();
            }
            catch (Exception ex)
            {
                string exMessage = ex.Message;
            }

            return RotateFlipBitmap(bitmap, RotateFlipType.RotateNoneFlipY);
        }

        public static int[,] ConvertBitmapTo2DIntArray(Bitmap img)
        {
            int[,] int2DArray = new int[img.Width, img.Height];
            try
            {
                // Already locked!
                //LockBitmap lockBitmap = new LockBitmap(img);
                //lockBitmap.LockBits();

                // Loop through the images pixels to reset color. 
                for (int x = 0; x < img.Width; x++)
                {
                    for (int y = 0; y < img.Height; y++)
                    {
                        Color pixelColor = img.GetPixel(x, y);
                        int2DArray[x, y] = (int)pixelColor.B;
                    }
                }

                //lockBitmap.UnlockBits();
            }
            catch (Exception ex)
            {
                string exMessage = ex.Message;
            }
            return int2DArray;

            //ImageConverter converter = new ImageConverter();
            //byte[] byteArray = (byte[])converter.ConvertTo(img, typeof(byte[]));
            //int[] intArray = Array.ConvertAll(byteArray, c => (int)c); 
            //return (Convert1DArrayTo2DArray(intArray, img.Width, img.Height));
        }

        public static Bitmap Convert2DIntArrayNewToBitmap(int[,] imgdat, PixelFormat pixelFormat = PixelFormat.Format24bppRgb) // PixelFormat.Format32bppRgb // int array is [width, height]
        {
            int i, j;
            int dataMin = 1000000000, dataMax = 0;
            // Make Sure Data is scaled from 0-255
            for (i = 0; i < imgdat.GetLength(0); i++)
            {
                for (j = 0; j < imgdat.GetLength(1); j++)
                {
                    dataMin = Math.Min(dataMin, imgdat[i, j]);
                    dataMax = Math.Max(dataMax, imgdat[i, j]);
                }
            }

            if (dataMin == 1000000000 || dataMax == 0 || (dataMax == dataMin))
            {
                return new Bitmap(imgdat.GetLength(0), imgdat.GetLength(1), pixelFormat);
            }

            float scale = 255 / (dataMax - dataMin);
            float offset = dataMin;

            Bitmap bmp = new Bitmap(imgdat.GetLength(0), imgdat.GetLength(1), pixelFormat);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            int stride = bmpData.Stride;
            int maxValue = 0;
            int scaledValue = 0;
            byte[] bytes = new byte[stride * bmp.Height];
            if (pixelFormat == PixelFormat.Format24bppRgb)
            {
                for (i = 0; i < bmp.Width; i++)
                {
                    for (j = 0; j < bmp.Height; j++)
                    {
                        if (imgdat[i, j] != 0)
                        {
                            maxValue = Math.Max(maxValue, imgdat[i, j]);
                        }
                        scaledValue = (int)Math.Min(0.0f, Math.Max(255.0f, ((float)imgdat[i, j] - offset) * scale));
                        //if (i == 350 && j == 143)
                        //{
                        //    int foo = 0;
                        //    foo += 1;
                        //}
                        Color color = Color.FromArgb(scaledValue);
                        bytes[(j * stride) + i * 3] = color.B;
                        bytes[(j * stride) + i * 3 + 1] = color.G;
                        bytes[(j * stride) + i * 3 + 2] = color.R;
                    }
                }
            }
            else // if (pixelFormat == PixelFormat.Format32bppRgb)
            {
                for (i = 0; i < bmp.Width; i++)
                {
                    for (j = 0; j < bmp.Height; j++)
                    {
                        if (imgdat[i, j] != 0)
                        {
                            maxValue = Math.Max(maxValue, imgdat[i, j]);
                        }
                        scaledValue = (int)Math.Min(0.0f, Math.Max(255.0f, ((float)imgdat[i, j] - offset) * scale));
                        //if (i == 350 && j == 143)
                        //{
                        //    int foo = 0;
                        //    foo += 1;
                        //}
                        Color color = Color.FromArgb(scaledValue);
                        bytes[(j * stride) + i * 4] = color.B;
                        bytes[(j * stride) + i * 4 + 1] = color.G;
                        bytes[(j * stride) + i * 4 + 2] = color.R;
                        bytes[(j * stride) + i * 4 + 3] = color.A;
                    }
                }
            }

            System.IntPtr scan0 = bmpData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(bytes, 0, scan0, stride * bmp.Height);
            bmp.UnlockBits(bmpData);

            return bmp;
        }

        public static Bitmap Convert2DIntArrayToBitmap(int[,] imgdat, PixelFormat pixelFormat = PixelFormat.Format24bppRgb) // PixelFormat.Format32bppRgb // int array is [width, height]
        {
            int i, j;

            Bitmap bmp = new Bitmap(imgdat.GetLength(0), imgdat.GetLength(1), pixelFormat);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            int stride = bmpData.Stride;
            int maxValue = 0;

            byte[] bytes = new byte[stride * bmp.Height];
            if (pixelFormat == PixelFormat.Format24bppRgb)
            {
                for (i = 0; i < bmp.Width; i++)
                {
                    for (j = 0; j < bmp.Height; j++)
                    {
                        if (imgdat[i, j] != 0)
                        {
                            maxValue = Math.Max(maxValue, imgdat[i, j]); 
                        }
                        Color color = Color.FromArgb(imgdat[i, j]);
                        bytes[(j * stride) + i * 3] = color.B;
                        bytes[(j * stride) + i * 3 + 1] = color.G;
                        bytes[(j * stride) + i * 3 + 2] = color.R;
                    }
                }
            }
            else // if (pixelFormat == PixelFormat.Format32bppRgb)
            {
                for (i = 0; i < bmp.Width; i++)
                {
                    for (j = 0; j < bmp.Height; j++)
                    {
                        if (imgdat[i, j] != 0)
                        {
                            maxValue = Math.Max(maxValue, imgdat[i, j]);
                        }
                        Color color = Color.FromArgb(imgdat[i, j]);
                        bytes[(j * stride) + i * 4] = color.B;
                        bytes[(j * stride) + i * 4 + 1] = color.G;
                        bytes[(j * stride) + i * 4 + 2] = color.R;
                        bytes[(j * stride) + i * 4 + 3] = color.A;
                    }
                }
            }

            System.IntPtr scan0 = bmpData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(bytes, 0, scan0, stride * bmp.Height);
            bmp.UnlockBits(bmpData);

            return bmp;
        }

        public static Bitmap Convert2DIntArrayToBitmap(int[,] imgdat, int imgdatMax, PixelFormat pixelFormat = PixelFormat.Format24bppRgb) // PixelFormat.Format32bppRgb // int array is [width, height]
        {
            int i, j;

            Bitmap bmp = new Bitmap(imgdat.GetLength(0), imgdat.GetLength(1), pixelFormat);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);
            int stride = bmpData.Stride;
            int maxValue = 0;
            int value = 0;

            byte[] bytes = new byte[stride * bmp.Height];
            if (pixelFormat == PixelFormat.Format24bppRgb)
            {
                for (i = 0; i < bmp.Width; i++)
                {
                    for (j = 0; j < bmp.Height; j++)
                    {
                        if (imgdat[i, j] != 0)
                        {
                            maxValue = Math.Max(maxValue, imgdat[i, j]);
                        }
                    }
                }

                for (i = 0; i < bmp.Width; i++)
                {
                    for (j = 0; j < bmp.Height; j++)
                    {
                        value = (255 * imgdat[j, i]) / maxValue; // NGE11192013 from imgdat[i, j] to imgdat[j, i]
                        bytes[(j * stride) + i * 3] = (byte)value;
                        bytes[(j * stride) + i * 3 + 1] = (byte)value;
                        bytes[(j * stride) + i * 3 + 2] = (byte)value;

                        //    Color color = Color.FromArgb(imgdat[i, j]);
                        //    bytes[(j * stride) + i * 3] = color.B;
                        //    bytes[(j * stride) + i * 3 + 1] = color.G;
                        //    bytes[(j * stride) + i * 3 + 2] = color.R;
                    }
                }
            }
            else // if (pixelFormat == PixelFormat.Format32bppRgb)
            {
                for (i = 0; i < bmp.Width; i++)
                {
                    for (j = 0; j < bmp.Height; j++)
                    {
                        if (imgdat[i, j] != 0)
                        {
                            maxValue = Math.Max(maxValue, imgdat[i, j]);
                        }
                    }
                }
                for (i = 0; i < bmp.Width; i++)
                {
                    for (j = 0; j < bmp.Height; j++)
                    {
                        value = (255 * imgdat[j, i]) / maxValue; // NGE11192013 from imgdat[i, j] to imgdat[j, i]
                        bytes[(j * stride) + i * 4] = (byte)value;
                        bytes[(j * stride) + i * 4 + 1] = (byte)value;
                        bytes[(j * stride) + i * 4 + 2] = (byte)value;
                        bytes[(j * stride) + i * 4 + 3] = 255;

                        //Color color = Color.FromArgb(imgdat[i, j]);
                        //bytes[(j * stride) + i * 4] = color.B;
                        //bytes[(j * stride) + i * 4 + 1] = color.G;
                        //bytes[(j * stride) + i * 4 + 2] = color.R;
                        //bytes[(j * stride) + i * 4 + 3] = color.A;
                    }
                }
            }

            System.IntPtr scan0 = bmpData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(bytes, 0, scan0, stride * bmp.Height);
            bmp.UnlockBits(bmpData);

            return bmp;
        }

        public static byte[] ConvertImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image ConvertByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        /// <summary>
        /// Method to resize, convert and save the image.
        /// </summary>
        /// <param name="image">Bitmap image.</param>
        /// <param name="maxWidth">resize width.</param>
        /// <param name="maxHeight">resize height.</param>
        /// <param name="quality">quality setting value.</param>
        /// <param name="filePath">file path.</param>      
        public void Save(Bitmap image, int maxWidth, int maxHeight, int quality, string filePath)
        {
            // Get the image's original width and height
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            // To preserve the aspect ratio
            float ratioX = (float)maxWidth / (float)originalWidth;
            float ratioY = (float)maxHeight / (float)originalHeight;
            float ratio = Math.Min(ratioX, ratioY);

            // New width and height based on aspect ratio
            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            // Convert other formats (including CMYK) to RGB.
            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            // Draws the image in the specified size with quality mode set to HighQuality
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            // Get an ImageCodecInfo object that represents the JPEG codec.
            ImageCodecInfo imageCodecInfo = GetEncoderInfo(ImageFormat.Jpeg);

            // Create an Encoder object for the Quality parameter.
            Encoder encoder = Encoder.Quality;

            // Create an EncoderParameters object. 
            EncoderParameters encoderParameters = new EncoderParameters(1);

            // Save the image as a JPEG file with quality level.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;
            newImage.Save(filePath, imageCodecInfo, encoderParameters);
        }

        /// <summary>
        /// Method to get encoder infor for given image format.
        /// </summary>
        /// <param name="format">Image format</param>
        /// <returns>image codec info.</returns>
        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }

        public static Bitmap ConvertTo24bpp(Image img)
        {
            var bmp = new Bitmap(img.Width, img.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (var gr = Graphics.FromImage(bmp))
                gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));
            return bmp;
        }

        public static Bitmap TranslateImage(Bitmap bmp)
        {
            Image newBitmap = new Bitmap(bmp.Width, bmp.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImageUnscaled(newBitmap, 0, 0, bmp.Width, bmp.Height);
            g.Dispose();
            return (Bitmap)newBitmap;
        }

        public static Bitmap TranslateAndScaleImage(Bitmap bitmap, PointF point, PointF scaleFactor)
        {
            if (bitmap == null) return null;
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                Matrix X = new Matrix();
                X.Translate(point.X, point.Y);
                X.Scale(scaleFactor.X, scaleFactor.Y);
                graphics.Transform = X;
                //Pen myPen = new Pen(Color.Red, 1);
                //graphics.DrawLine(myPen, 90, 0, 100, 100);
                graphics.DrawImage(bitmap, 0, 0);
                // graphics gets Disposed at the end of using statement automatically
            }
            return newBitmap;
        }

        public static Bitmap ClipImage(Bitmap bitmap, Rectangle rect)
        {
            if (bitmap == null) return null;
            Bitmap newBitmap = new Bitmap(rect.Width, rect.Height);

            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                graphics.DrawImage(bitmap, rect.X, rect.Y);
                // graphics gets Disposed at the end of using statement automatically
            }
            return newBitmap;
        }

        public static Bitmap DrawThresholdsOnImage(Bitmap bitmap, PointF thresholds, PointF thresholdsAuto, PointF scaleFactors, PointF offsets, bool HasLowQualityScore)
        {
            if (bitmap == null) return null;
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            PointF[] thresholdPoint = new PointF[2];
            int widthMinusOne = bitmap.Width - 1;
            int heightMinusOne = bitmap.Height - 1;

            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                graphics.DrawImage(bitmap, 0, 0);
                Pen myPen = new Pen(Color.Red, 1);
                if (HasLowQualityScore == true) // draw a Yellow rect around the image
                {
                    myPen = new Pen(Color.Yellow, 3);
                    graphics.DrawRectangle(myPen, 0, 0, bitmap.Width, bitmap.Height); // horizontal threshold
                }

                thresholdPoint[1].X = (int)((thresholds.X - offsets.X) / scaleFactors.X);
                thresholdPoint[0].X = (int)((thresholds.X - offsets.X) / scaleFactors.X);
                thresholdPoint[0].Y = 0;
                thresholdPoint[1].Y = bitmap.Height - 1;

                if (thresholds.X != thresholdsAuto.X)
                {
                    if (thresholds.X == 0 || thresholds.X >= widthMinusOne)
                        myPen = new Pen(Color.Magenta, 3);
                    else
                        myPen = new Pen(Color.Magenta, 1);
                }
                else
                {
                    if (thresholds.X == 0 || thresholds.X >= widthMinusOne)
                        myPen = new Pen(Color.Red, 3);
                    else
                        myPen = new Pen(Color.Red, 1);
                }
                graphics.DrawLine(myPen, thresholdPoint[0], thresholdPoint[1]); // vertical threshold

                thresholdPoint[1].Y = (bitmap.Height - 1) - (int)((thresholds.Y - offsets.Y) / scaleFactors.Y);
                thresholdPoint[0].Y = (bitmap.Height - 1) - (int)((thresholds.Y - offsets.Y) / scaleFactors.Y);
                thresholdPoint[0].X = 0;
                thresholdPoint[1].X = bitmap.Width - 1;

                if (thresholds.Y != thresholdsAuto.Y)
                {
                    if (thresholds.Y == 0 || thresholds.Y >= heightMinusOne)
                        myPen = new Pen(Color.Magenta, 3);
                    else
                        myPen = new Pen(Color.Magenta, 1);
                }
                else
                {
                    if (thresholds.Y == 0 || thresholds.Y >= heightMinusOne)
                        myPen = new Pen(Color.Red, 3);
                    else
                        myPen = new Pen(Color.Red, 1);
                }
                graphics.DrawLine(myPen, thresholdPoint[0], thresholdPoint[1]); // horizontal threshold
                // graphics gets Disposed at the end of using statement automatically
            }
            return newBitmap;
        }

        public static int[] ApplySavitzkyGolayFilter(int[] intData, int smoothingWindow, int nDegreePolynomial)
        {
            int nPoints = smoothingWindow; // NGE02112014  10;
            //nDegreePolynomial = 4;
            double[] coeffs = SGFilter.computeSGCoefficients(nPoints, nPoints, nDegreePolynomial, 0); // NOTE!!! If you want to use the Savitzky Golay for derivatives, then the you must multiply the data array by the order of the derivative factorial. Example First order 1, second order 2 * 1, third order 3 * 2 * 1
            SGFilter sgFilter = new SGFilter(nPoints, nPoints);
            int length = intData.Length;
            double[] data = new double[length];
            int[] ret = new int[length];
            int i;
            for (i = 0; i < length; i++)
            {
                data[i] = intData[i];
            }

            double[] smooth = sgFilter.smooth(data, new double[0], new double[0], coeffs); // No left of right padding
            for (i = 0; i < length; i++)
            {
                ret[i] = (int)(smooth[i] + 0.5f);
            }
            return ret;
        }

        public static Bitmap DrawHistogramsOnImage(Bitmap bitmap, int[] histogramX, int[] histogramY, int penThickness, Color colorX, Color colorY, bool autoScale, bool smooth, int smoothingWindow, bool showX, bool showY, bool IsDashedLine = false)
        {
            if (bitmap == null) return null;
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                // Cycle through the IntensityMap and create a histogram
                int i, j;
                Point[] pointsX = new Point[bitmap.Width];
                Point[] pointsY = new Point[bitmap.Height];

                float maxX = 0, maxY = 0;
                float minX = 255, minY = 255;
                for (i = 0; i < bitmap.Width; i++)
                {
                    pointsX[i].X = i;
                    pointsX[i].Y = histogramX[i];
                    maxX = Math.Max(histogramX[i], maxX);
                    minX = Math.Min(histogramX[i], minX);
                }
                for (j = 0; j < bitmap.Height; j++)
                {
                    pointsY[j].Y = j;
                    pointsY[j].X = histogramY[j];
                    maxY = Math.Max(histogramY[j], maxY);
                    minY = Math.Min(histogramY[j], minY);
                }

                if (maxX == minX) minX = 0;
                if (maxY == minY) minY = 0;
                if (maxX == 0.0f) maxX = 255; // bitmap.Width;
                if (maxY == 0.0f) maxY = 255; // bitmap.Height;

                PointF histogramScale = new PointF(((float)bitmap.Height) / (maxX - minX), ((float)bitmap.Width) / (maxY - minY));

                if (smooth == true && autoScale == true)
                {
                    for (i = 0; i < bitmap.Width; i++)
                    {
                        histogramX[i] = (int)((float)(histogramX[i] - minX) * histogramScale.X);
                        pointsX[i].Y = histogramX[i];
                    }
                    for (j = 0; j < bitmap.Height; j++)
                    {
                        histogramY[j] = (int)((float)(histogramY[j] - minY) * histogramScale.Y);
                        pointsY[j].X = histogramY[j];
                    }

                    if (smoothingWindow > 0)
                    {
                        histogramX = ApplySavitzkyGolayFilter(histogramX, smoothingWindow, 4);
                        histogramY = ApplySavitzkyGolayFilter(histogramY, smoothingWindow, 4);
                    }
                    
                    // Set the beginning and end smoothingWindow to the same value
                    int lengthMinusOne = histogramX.Length - 1;
                    for (i = 0; i < smoothingWindow; i++)
                    {
                        histogramX[i] = histogramX[smoothingWindow];
                        histogramX[lengthMinusOne - i] = histogramX[lengthMinusOne - smoothingWindow];
                    }
                    lengthMinusOne = histogramY.Length - 1;
                    for (i = 0; i < smoothingWindow; i++)
                    {
                        histogramY[i] = histogramY[smoothingWindow];
                        histogramY[lengthMinusOne - i] = histogramY[lengthMinusOne - smoothingWindow];
                    }

                    for (i = 0; i < bitmap.Width; i++)
                    {
                        pointsX[i].Y = histogramX[i];
                    }
                    for (j = 0; j < bitmap.Height; j++)
                    {
                        pointsY[j].X = histogramY[j];
                    }
                }
                else if (smooth == true)
                {
                    if (smoothingWindow > 0)
                    {
                        histogramX = ApplySavitzkyGolayFilter(histogramX, smoothingWindow, 4);
                        histogramY = ApplySavitzkyGolayFilter(histogramY, smoothingWindow, 4);
                    }

                    // Set the beginning and end smoothingWindow to the same value
                    int lengthMinusOne = histogramX.Length - 1;
                    for (i = 0; i < smoothingWindow; i++)
                    {
                        histogramX[i] = histogramX[smoothingWindow];
                        histogramX[lengthMinusOne - i] = histogramX[lengthMinusOne - smoothingWindow];
                    }
                    lengthMinusOne = histogramY.Length - 1;
                    for (i = 0; i < smoothingWindow; i++)
                    {
                        histogramY[i] = histogramY[smoothingWindow];
                        histogramY[lengthMinusOne - i] = histogramY[lengthMinusOne - smoothingWindow];
                    }
                    
                    for (i = 0; i < bitmap.Width; i++)
                    {
                        pointsX[i].Y = histogramX[i];
                    }
                    for (j = 0; j < bitmap.Height; j++)
                    {
                        pointsY[j].X = histogramY[j];
                    }
                }
                else if (autoScale == true)
                {
                    for (i = 0; i < bitmap.Width; i++)
                    {
                        pointsX[i].Y = (int)((float)(histogramX[i] - minX) * histogramScale.X);
                    }
                    for (j = 0; j < bitmap.Height; j++)
                    {
                        pointsY[j].X = (int)((float)(histogramY[j] - minY) * histogramScale.Y);
                    }
                }

                //for (i = 0; i < bitmap.Width; i++)
                //{
                //    pointsX[i].X = i;
                //    pointsX[i].Y = (bitmap.Height - 1) - (int)((Math.Log10(1.0 + (float)pointsX[i].Y * histogramScale.Y) / Math.Log10(2.0)));
                //}
                //for (j = 0; j < bitmap.Height; j++)
                //{
                //    pointsY[j].Y = j;
                //    pointsY[j].X = (int)(Math.Log10(1.0 + (float)pointsY[j].X * histogramScale.X) / Math.Log10(2.0));
                //}

                //for (i = 0; i < bitmap.Width; i++)
                //{
                //    pointsX[i].X = i;
                //    if ((float)pointsX[i].Y * histogramScale.Y > 1.0)
                //        pointsX[i].Y = (bitmap.Height - 1) - (int)((Math.Log10((float)pointsX[i].Y * histogramScale.Y)) * 4.0f);
                //    else
                //        pointsX[i].Y = bitmap.Height - 1;
                //}
                //for (j = 0; j < bitmap.Height; j++)
                //{
                //    pointsY[j].Y = j;
                //    if ((float)(float)pointsY[j].X * histogramScale.X > 1.0)
                //        pointsY[j].X = (int)((Math.Log10((float)pointsY[j].X * histogramScale.X)) * 4.0f);
                //    else
                //        pointsY[j].X = 0;
                //}

                graphics.DrawImage(bitmap, 0, 0);
                float[] dashValues = { 3, 3 }; //{ 5, 2, 15, 4 };
                Pen myPen;
                if (showX)
                {
                    myPen = new Pen(colorX, penThickness); // X horizontal
                    if (IsDashedLine == true)
                    {
                        myPen.DashPattern = dashValues;
                    }
                    graphics.DrawCurve(myPen, pointsX); // horizontal threshold
                }
                if (showY)
                {
                    myPen = new Pen(colorY, penThickness); // Y vertical
                    if (IsDashedLine == true)
                    {
                        myPen.DashPattern = dashValues;
                    }
                    graphics.DrawCurve(myPen, pointsY); // horizontal threshold
                }
                // graphics gets Disposed at the end of using statement automatically
            }
            return newBitmap;
        }

        public static Bitmap DrawHistogramsOnImage(Bitmap bitmap)
        {
            if (bitmap == null) return null;
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                // Cycle through the IntensityMap and create a histogram
                int[] histogramX = new int[bitmap.Width];
                int[] histogramY = new int[bitmap.Height];
                int i, j;
                Point[] pointsX = new Point[bitmap.Width];
                Point[] pointsY = new Point[bitmap.Height];

                int[,] IntensityMap = ConvertBitmapTo2DIntArray(bitmap);

                float maxX = 0, maxY = 0;
                for (i = 0; i < bitmap.Width; i++)
                {
                    for (j = 0; j < bitmap.Height; j++)
                    {
                        pointsX[i].Y += IntensityMap[i, j];
                        pointsY[j].X += IntensityMap[i, j];
                        maxX = Math.Max(pointsY[j].X, maxX);
                        maxY = Math.Max(pointsX[i].Y, maxY);
                    }
                }
                if (maxX == 0.0f) maxX = 10000.0f;
                if (maxY == 0.0f) maxY = 10000.0f;

                PointF histogramScale = new PointF(10000.0f / maxX, 10000.0f / maxY);
                //for (i = 0; i < bitmap.Width; i++)
                //{
                //    pointsX[i].X = i;
                //    pointsX[i].Y = (bitmap.Height - 1) - (int)((Math.Log10(1.0 + (float)pointsX[i].Y * histogramScale.Y) / Math.Log10(2.0)));
                //}
                //for (j = 0; j < bitmap.Height; j++)
                //{
                //    pointsY[j].Y = j;
                //    pointsY[j].X = (int)(Math.Log10(1.0 + (float)pointsY[j].X * histogramScale.X) / Math.Log10(2.0));
                //}

                for (i = 0; i < bitmap.Width; i++)
                {
                    pointsX[i].X = i;
                    if ((float)pointsX[i].Y * histogramScale.Y > 1.0)
                        pointsX[i].Y = (bitmap.Height - 1) - (int)((Math.Log10((float)pointsX[i].Y * histogramScale.Y)) * 4.0f);
                    else
                        pointsX[i].Y = bitmap.Height - 1;
                }
                for (j = 0; j < bitmap.Height; j++)
                {
                    pointsY[j].Y = j;
                    if ((float)(float)pointsY[j].X * histogramScale.X > 1.0)
                        pointsY[j].X = (int)((Math.Log10((float)pointsY[j].X * histogramScale.X)) * 4.0f);
                    else
                        pointsY[j].X = 0;
                }

                graphics.DrawImage(bitmap, 0, 0);
                Pen myPen = new Pen(Color.Blue, 1); // X horizontal
                graphics.DrawCurve(myPen, pointsX); // horizontal threshold
                myPen = new Pen(Color.Green, 1); // Y vertical
                graphics.DrawCurve(myPen, pointsY); // horizontal threshold
                // graphics gets Disposed at the end of using statement automatically
            }
            return newBitmap;
        }

        public static Bitmap DrawScaledHistogramsOnImage(Bitmap bitmap)
        {
            if (bitmap == null) return null;
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                // Cycle through the IntensityMap and create a histogram
                int[] histogramX = new int[bitmap.Width];
                int[] histogramY = new int[bitmap.Height];
                int i, j;
                Point[] pointsX = new Point[bitmap.Width];
                Point[] pointsY = new Point[bitmap.Height];

                int[,] IntensityMap = ConvertBitmapTo2DIntArray(bitmap);

                float maxX = 0, maxY = 0;
                for (i = 0; i < bitmap.Width; i++)
                {
                    for (j = 0; j < bitmap.Height; j++)
                    {
                        pointsX[i].Y += IntensityMap[i, j];
                        pointsY[j].X += IntensityMap[i, j];
                        maxX = Math.Max(pointsY[j].X, maxX);
                        maxY = Math.Max(pointsX[i].Y, maxY);
                    }
                }
                if (maxX == 0.0f) maxX = (float)bitmap.Width * 255.0f; // 10000.0f;
                if (maxY == 0.0f) maxY = (float)bitmap.Height * 255.0f; // 10000.0f;
                // Scale from 0-255 intensity and then scale to bitmap size

                // Max X value is intensity (255) * bitmath.Width
                // Max Y value is intensity (255) * bitmath.Height

                PointF histogramScale = new PointF((1.0f / 255.0f), (1.0f / 255.0f));
                // PointF histogramScale = new PointF(10000.0f / maxX, 10000.0f / maxY);
                //for (i = 0; i < bitmap.Width; i++)
                //{
                //    pointsX[i].X = i;
                //    pointsX[i].Y = (bitmap.Height - 1) - (int)((Math.Log10(1.0 + (float)pointsX[i].Y * histogramScale.Y) / Math.Log10(2.0)));
                //}
                //for (j = 0; j < bitmap.Height; j++)
                //{
                //    pointsY[j].Y = j;
                //    pointsY[j].X = (int)(Math.Log10(1.0 + (float)pointsY[j].X * histogramScale.X) / Math.Log10(2.0));
                //}

                for (i = 0; i < bitmap.Width; i++)
                {
                    pointsX[i].X = i;
                    if ((float)pointsX[i].Y * histogramScale.Y > 1.0)
                        pointsX[i].Y = (bitmap.Height - 1) - (int)((float)pointsX[i].Y * histogramScale.Y); //pointsX[i].Y = (bitmap.Height - 1) - (int)((Math.Log10((float)pointsX[i].Y * histogramScale.Y)) * 4.0f);
                    else
                        pointsX[i].Y = bitmap.Height - 1;
                }
                for (j = 0; j < bitmap.Height; j++)
                {
                    pointsY[j].Y = j;
                    if ((float)(float)pointsY[j].X * histogramScale.X > 1.0)
                        pointsY[j].X = (int)(((float)pointsY[j].X * histogramScale.X)); //pointsY[j].X = (int)((Math.Log10((float)pointsY[j].X * histogramScale.X)) * 4.0f);
                    else
                        pointsY[j].X = 0;
                }

                graphics.DrawImage(bitmap, 0, 0);
                Pen myPen = new Pen(Color.Blue, 1); // X horizontal
                graphics.DrawCurve(myPen, pointsX); // horizontal threshold
                myPen = new Pen(Color.Green, 1); // Y vertical
                graphics.DrawCurve(myPen, pointsY); // horizontal threshold
                // graphics gets Disposed at the end of using statement automatically
            }
            return newBitmap;
        }

        public static Bitmap DrawHistogramsAndThresholdsOnImage(Bitmap bitmap, PointF thresholds, PointF offsets, PointF scaleFactors)
        {
            if (bitmap == null) return null;
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            PointF[] thresholdPoint = new PointF[2];

            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                graphics.DrawImage(bitmap, 0, 0);

                // Cycle through the IntensityMap and create a histogram
                int[] histogramX = new int[bitmap.Width];
                int[] histogramY = new int[bitmap.Height];
                int i, j;
                Point[] pointsX = new Point[bitmap.Width];
                Point[] pointsY = new Point[bitmap.Height];

                int[,] IntensityMap = ConvertBitmapTo2DIntArray(bitmap);

                float maxX = 0, maxY = 0;
                for (i = 0; i < bitmap.Width; i++)
                {
                    for (j = 0; j < bitmap.Height; j++)
                    {
                        pointsX[i].Y += IntensityMap[i, j];
                        pointsY[j].X += IntensityMap[i, j];
                        maxX = Math.Max(pointsY[j].X, maxX);
                        maxY = Math.Max(pointsX[i].Y, maxY);
                    }
                }
                if (maxX == 0.0f) maxX = 10000.0f;
                if (maxY == 0.0f) maxY = 10000.0f;

                PointF histogramScale = new PointF(10000.0f / maxX, 10000.0f / maxY);
                for (i = 0; i < bitmap.Width; i++)
                {
                    pointsX[i].X = i;
                    if ((float)pointsX[i].Y * histogramScale.Y > 1.0)
                        pointsX[i].Y = (bitmap.Height - 1) - (int)((Math.Log10((float)pointsX[i].Y * histogramScale.Y)) * 4.0f);
                    else
                        pointsX[i].Y = bitmap.Height - 1;
                }
                for (j = 0; j < bitmap.Height; j++)
                {
                    pointsY[j].Y = j;
                    if ((float)(float)pointsY[j].X * histogramScale.X > 1.0)
                        pointsY[j].X = (int)((Math.Log10((float)pointsY[j].X * histogramScale.X)) * 4.0f);
                    else
                        pointsY[j].X = 0;
                }

                Pen myPen = new Pen(Color.Blue, 1); // X horizontal
                graphics.DrawCurve(myPen, pointsX); // horizontal threshold
                myPen = new Pen(Color.Green, 1); // Y vertical
                graphics.DrawCurve(myPen, pointsY); // horizontal threshold

                // Calculate and draw thresholds
                thresholdPoint[1].X = (int)((thresholds.X - offsets.X) / scaleFactors.X);
                thresholdPoint[0].X = thresholdPoint[1].X = (int)((thresholds.X - offsets.X) / scaleFactors.X);
                thresholdPoint[0].Y = 0;
                thresholdPoint[1].Y = bitmap.Height - 1;

                myPen = new Pen(Color.Red, 1);
                graphics.DrawLine(myPen, thresholdPoint[0], thresholdPoint[1]); // horizontal threshold

                thresholdPoint[1].Y = (bitmap.Height - 1) - (int)((thresholds.Y - offsets.Y) / scaleFactors.Y);
                thresholdPoint[0].Y = (bitmap.Height - 1) - (int)((thresholds.Y - offsets.Y) / scaleFactors.Y);
                thresholdPoint[0].X = 0;
                thresholdPoint[1].X = bitmap.Width - 1;

                graphics.DrawLine(myPen, thresholdPoint[0], thresholdPoint[1]); // vertical threshold
                // graphics gets Disposed at the end of using statement automatically
            }
            return newBitmap;
        }

        public static Bitmap DrawDataOnImage(Bitmap bitmap, int[] data1, int[] data2)
        {
            if (bitmap == null) return null;
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                graphics.DrawImage(bitmap, 0, 0);

                // Cycle through the histogram and make sure it is the proper scale 
                if (data1.Length > bitmap.Width || data2.Length > bitmap.Width)
                {
                    return newBitmap; // Maybe make this work later
                }
                if (data1.Length != data2.Length)
                {
                    return newBitmap; // Maybe make this work later
                }
                int offset = (bitmap.Width - data1.Length) / 2;

                double maxHisX = -1000000000;
                double minHisX = 1000000000;
                double maxHisValX = (double)(bitmap.Height - 1); // 63

                for (int i = 0; i < data1.Length; i++)
                {
                    maxHisX = Math.Max(maxHisX, data1[i]);
                    minHisX = Math.Min(minHisX, data1[i]);
                }

                double maxHisY = -1000000000;
                double minHisY = 1000000000;
                double maxHisValY = (double)(bitmap.Height - 1); // 63

                for (int i = 0; i < data2.Length; i++)
                {
                    maxHisY = Math.Max(maxHisY, data2[i]);
                    minHisY = Math.Min(minHisY, data2[i]);
                }

                // Need to scale from 0-63
                double rangeHisX = maxHisX - minHisX;

                Pen myPen;
                if (rangeHisX > 0)
                {
                    Point[] pointsX = new Point[data1.Length];
                    for (int i = 0; i < data1.Length; i++)
                    {
                        pointsX[i] = new Point(i + offset, (int)(maxHisValX - (((data1[i] - minHisX) / rangeHisX) * maxHisValX))); // now 0 - newMax (either 63 or 255) values for array
                    }
                    //myPen = new Pen(Color.FromArgb(127, 255, 255, 0), 1); // X horizontal
                    myPen = new Pen(Color.FromArgb(255, 255, 0, 0), 1); // X horizontal
                    //Pen myPen = new Pen(Color.Yellow, 1); // X horizontal
                    graphics.DrawCurve(myPen, pointsX); // 
                }

                // Need to scale from 0-63
                double rangeHisY = maxHisY - minHisY;

                if (rangeHisY > 0)
                {
                    Point[] pointsY = new Point[data2.Length];
                    for (int i = 0; i < data2.Length; i++)
                    {
                        pointsY[i] = new Point(i + offset, (int)(maxHisValY - (((data2[i] - minHisY) / rangeHisY) * maxHisValY))); // now 0 - newMax (either 63 or 255) values for array
                    }
                    //myPen = new Pen(Color.FromArgb(127, 0, 0, 255), 1); // Y horizontal
                    myPen = new Pen(Color.FromArgb(255, 0, 0, 255), 1); // Y horizontal
                    graphics.DrawCurve(myPen, pointsY); // 
                }

                // graphics gets Disposed at the end of using statement automatically
            }
            return newBitmap;
        }

        public static Bitmap DrawHistogramsAndThresholdsOnImage(Bitmap bitmap, int[] thresholds, int[] histogramX, int[] histogramY)
        {
            if (bitmap == null) return null;
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                graphics.DrawImage(bitmap, 0, 0);

                // Cycle through the histogram and make sure it is the proper scale 
                if (histogramX.Length > bitmap.Width || histogramY.Length > bitmap.Width)
                {
                    return newBitmap; // Maybe make this work later
                }
                if (histogramX.Length != histogramY.Length)
                {
                    return newBitmap; // Maybe make this work later
                }
                int offset = (bitmap.Width - histogramX.Length) / 2;

                double maxHisX = -1000000000;
                double minHisX = 1000000000;
                double maxHisValX = (double)(bitmap.Height - 1); // 63

                for (int i = 0; i < histogramX.Length; i++)
                {
                    maxHisX = Math.Max(maxHisX, histogramX[i]);
                    minHisX = Math.Min(minHisX, histogramX[i]);
                }

                double maxHisY = -1000000000;
                double minHisY = 1000000000;
                double maxHisValY = (double)(bitmap.Height - 1); // 63

                for (int i = 0; i < histogramY.Length; i++)
                {
                    maxHisY = Math.Max(maxHisY, histogramY[i]);
                    minHisY = Math.Min(minHisY, histogramY[i]);
                }

                // Need to scale from 0-63
                double rangeHisX = maxHisX - minHisX;

                Pen myPen;
                if (rangeHisX > 0)
                {
                    Point[] pointsX = new Point[histogramX.Length];
                    for (int i = 0; i < histogramX.Length; i++)
                    {
                        pointsX[i] = new Point(i + offset, (int)(maxHisValX - (((histogramX[i] - minHisX) / rangeHisX) * maxHisValX))); // now 0 - newMax (either 63 or 255) values for array
                    }
                    myPen = new Pen(Color.FromArgb(195, 0, 230, 0), 1); 
                    // myPen = new Pen(Color.FromArgb(195, 255, 255, 0), 1); // Yellow
                    //Pen myPen = new Pen(Color.Yellow, 1); // X horizontal
                    graphics.DrawCurve(myPen, pointsX); // 
                }

                // Need to scale from 0-63
                double rangeHisY = maxHisY - minHisY;

                if (rangeHisY > 0)
                {
                    Point[] pointsY = new Point[histogramY.Length];
                    for (int i = 0; i < histogramY.Length; i++)
                    {
                        pointsY[i] = new Point(i + offset, (int)(maxHisValY - (((histogramY[i] - minHisY) / rangeHisY) * maxHisValY))); // now 0 - newMax (either 63 or 255) values for array
                    }
                    myPen = new Pen(Color.FromArgb(195, 0, 255, 0), 1); // Y horizontal
                    // NGE03122014 graphics.DrawCurve(myPen, pointsY); // Do not draw original histogram for now 
                }

                PointF[] thresholdPoint = new PointF[2];
                for (int i = 0; i < thresholds.Length; i++)
                {
                    // Calculate and draw thresholds
                    thresholdPoint[1].X = thresholds[i];
                    thresholdPoint[0].X = thresholds[i];
                    thresholdPoint[0].Y = 0;
                    thresholdPoint[1].Y = bitmap.Height - 1;

                    if (i == 0) // eDeriv Blue
                        myPen = new Pen(Color.FromArgb(195, 0, 0, 255), 1); // X horizontal
                    else // if (i == 1) // Peak Red
                        myPen = new Pen(Color.FromArgb(195, 255, 0, 0), 1); // X horizontal
                    //myPen = new Pen(Color.Red, 1);
                    graphics.DrawLine(myPen, thresholdPoint[0], thresholdPoint[1]); // horizontal threshold
                }

                // graphics gets Disposed at the end of using statement automatically
            }
            return newBitmap;
        }

        public static Bitmap DrawHistogramsAndThresholdOnImage(Bitmap bitmap, int threshold, int[] histogramX, int[] histogramY)
        {
            if (bitmap == null) return null;
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                graphics.DrawImage(bitmap, 0, 0);

                // Cycle through the histogram and make sure it is the proper scale 
                if (histogramX.Length > bitmap.Width || histogramY.Length > bitmap.Width)
                {
                    return newBitmap; // Maybe make this work later
                }
                if (histogramX.Length != histogramY.Length)
                {
                    return newBitmap; // Maybe make this work later
                }
                int offset = (bitmap.Width - histogramX.Length) / 2;

                double maxHisX = -1000000000;
                double minHisX = 1000000000;
                double maxHisValX = (double)(bitmap.Height - 1); // 63

                for (int i = 0; i < histogramX.Length; i++)
                {
                    maxHisX = Math.Max(maxHisX, histogramX[i]);
                    minHisX = Math.Min(minHisX, histogramX[i]);
                }

                double maxHisY = -1000000000;
                double minHisY = 1000000000;
                double maxHisValY = (double)(bitmap.Height - 1); // 63

                for (int i = 0; i < histogramY.Length; i++)
                {
                    maxHisY = Math.Max(maxHisY, histogramY[i]);
                    minHisY = Math.Min(minHisY, histogramY[i]);
                }
                
                // Need to scale from 0-63
                double rangeHisX = maxHisX - minHisX;

                Pen myPen;
                if (rangeHisX > 0)
                {
                    Point[] pointsX = new Point[histogramX.Length];
                    for (int i = 0; i < histogramX.Length; i++)
                    {
                        pointsX[i] = new Point(i + offset, (int)(maxHisValX - (((histogramX[i] - minHisX) / rangeHisX) * maxHisValX))); // now 0 - newMax (either 63 or 255) values for array
                    }
                    myPen = new Pen(Color.FromArgb(127, 255, 255, 0), 1); // X horizontal
                    //Pen myPen = new Pen(Color.Yellow, 1); // X horizontal
                    graphics.DrawCurve(myPen, pointsX); // 
                }

                // Need to scale from 0-63
                double rangeHisY = maxHisY - minHisY;

                if (rangeHisY > 0)
                {
                    Point[] pointsY = new Point[histogramY.Length];
                    for (int i = 0; i < histogramY.Length; i++)
                    {
                        pointsY[i] = new Point(i + offset, (int)(maxHisValY - (((histogramY[i] - minHisY) / rangeHisY) * maxHisValY))); // now 0 - newMax (either 63 or 255) values for array
                    }
                    myPen = new Pen(Color.FromArgb(127, 0, 0, 255), 1); // Y horizontal
                    graphics.DrawCurve(myPen, pointsY); // 
                }

                PointF[] thresholdPoint = new PointF[2];
                    // Calculate and draw thresholds
                    thresholdPoint[1].X = threshold;
                    thresholdPoint[0].X = threshold;
                    thresholdPoint[0].Y = 0;
                    thresholdPoint[1].Y = bitmap.Height - 1;

                    myPen = new Pen(Color.FromArgb(127, 255, 0, 0), 1); // X horizontal
                    //myPen = new Pen(Color.Red, 1);
                    graphics.DrawLine(myPen, thresholdPoint[0], thresholdPoint[1]); // horizontal threshold

                    // graphics gets Disposed at the end of using statement automatically
            }
            return newBitmap;
        }

        public static Bitmap DrawHistogramAndThresholdOnImage(Bitmap bitmap, int threshold, int[] histogramX)
        {
            if (bitmap == null) return null;
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            using (Graphics graphics = Graphics.FromImage(newBitmap))
            {
                graphics.DrawImage(bitmap, 0, 0);

                // Cycle through the histogram and make sure it is the proper scale 
                if (histogramX.Length > bitmap.Width)
                {
                    return newBitmap; // Maybe make this work later
                }
                int offset = (bitmap.Width - histogramX.Length) / 2;

                double maxHis = -1000000000;
                double minHis = 1000000000;
                double maxHisVal = (double)(bitmap.Height - 1); // 63

                for (int i = 0; i < histogramX.Length; i++)
                {
                    maxHis = Math.Max(maxHis, histogramX[i]);
                    minHis = Math.Min(minHis, histogramX[i]);
                }
                // Need to scale from 0-63
                double rangeHis = maxHis - minHis;
                if (rangeHis > 0)
                {
                    Point[] pointsX = new Point[histogramX.Length];
                    for (int i = 0; i < histogramX.Length; i++)
                    {
                        pointsX[i] = new Point(i + offset, (int)(maxHisVal - (((histogramX[i] - minHis) / rangeHis) * maxHisVal))); // now 0 - newMax (either 63 or 255) values for array
                    }
                    Pen myPen = new Pen(Color.FromArgb(127, 255, 255, 0), 1); // X horizontal
                    //Pen myPen = new Pen(Color.Yellow, 1); // X horizontal
                    graphics.DrawCurve(myPen, pointsX); // 

                    PointF[] thresholdPoint = new PointF[2];
                    // Calculate and draw thresholds
                    thresholdPoint[1].X = threshold;
                    thresholdPoint[0].X = threshold;
                    thresholdPoint[0].Y = 0;
                    thresholdPoint[1].Y = bitmap.Height - 1;

                    myPen = new Pen(Color.FromArgb(127, 255, 0, 0), 1); // X horizontal
                    //myPen = new Pen(Color.Red, 1);
                    graphics.DrawLine(myPen, thresholdPoint[0], thresholdPoint[1]); // horizontal threshold

                   // graphics gets Disposed at the end of using statement automatically
                }
            }
            return newBitmap;
        }

        public static Bitmap Invert(Bitmap _currentBitmap)
        {
            Bitmap temp = (Bitmap)_currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();

            //LockBitmap lockBitmap = new LockBitmap(bmap);
            //lockBitmap.LockBits();

            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    bmap.SetPixel(i, j, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                }
            }
            // _currentBitmap = (Bitmap)bmap.Clone();

            //lockBitmap.UnlockBits();
            
            return bmap;
        }

        // NOTE:  This was slow, but is now much faster now that LockBits is used!
        public static Bitmap SetBlackToWhite(Bitmap _currentBitmap)
        {
            Bitmap temp = (Bitmap)_currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            
            LockBitmap lockBitmap = new LockBitmap(bmap);
            lockBitmap.LockBits();

            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    if (c.R == 0 && c.G == 0 && c.B == 0)
                        bmap.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                }
            }
            // _currentBitmap = (Bitmap)bmap.Clone();

            lockBitmap.UnlockBits();

            return bmap;
        }

        public static Bitmap AutoScale(Bitmap _currentBitmap)
        {
            Bitmap bmap = (Bitmap)_currentBitmap;

            LockBitmap lockBitmap = new LockBitmap(bmap);
            lockBitmap.LockBits();

            Color c;
            // Find min point on X axis with non 0 data
            // Find max point on X axis with non 0 data
            // Find min point on Y axis with non 0 data
            // Find max point on Y axis with non 0 data
            int i, j;
            int widthMinus1 = bmap.Width - 1;
            int heightMinus1 = bmap.Height - 1;
            int xMin = widthMinus1, yMin = heightMinus1;
            int xMax = 0, yMax = 0;
            for (i = 0; i < bmap.Width; i++)
            {
                for (j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    if (c.R != 0 || c.G != 0 || c.B != 0)
                    {
                        xMin = Math.Min(xMin, i);
                        yMin = Math.Min(yMin, j);
                    }
                }
            }
            for (i = widthMinus1; i >= 0; i--)
            {
                for (j = heightMinus1; j >= 0; j--)
                {
                    c = bmap.GetPixel(i, j);
                    if (c.R != 0 || c.G != 0 || c.B != 0)
                    {
                        xMax = Math.Max(xMax, i);
                        yMax = Math.Max(yMax, j);
                    }
                }
            }

            lockBitmap.UnlockBits();

            return TranslateAndScaleImage(bmap, new PointF(-xMin, -yMax), new PointF(Math.Abs((float)bmap.Width / (float)(xMax - xMin)), Math.Abs((float)bmap.Height / (float)(yMax - yMin)))); // , InterpolationMode.HighQualityBicubic, bmap.PixelFormat);
        }

        public static Bitmap AddBorder(Bitmap image, Color color, int size)
        {
            Bitmap result = new Bitmap(image.Width + size * 2, image.Height + size * 2);
            Graphics g = Graphics.FromImage(result);
            g.Clear(color);
            int x = (result.Width - image.Width) / 2;
            int y = (result.Height - image.Height) / 2;
            g.DrawImage(image, new Rectangle(x, y, image.Width, image.Height));
            g.Dispose();
            return result;
        }

        public static void DrawTextOnBitmap(Bitmap bmp, string txt, string fontname, int fontsize, Color bgcolor, Color fcolor)
        {
            //write to new Bitmap
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                Font font = new Font(fontname, fontsize);
                graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                graphics.DrawString(txt, font, new SolidBrush(fcolor), 0, 0);
                graphics.Flush();
                font.Dispose(); // This is automatically done in a using statement
                graphics.Dispose(); // This is automatically done in a using statement
            }
        }

        public static Bitmap DrawTextBelowBitmap(Bitmap bmp, string txt, string fontname, int fontsize, Color bgcolor, Color fcolor)
        {
            Bitmap textBmp = ConvertTextToImage(txt, fontname, fontsize, bgcolor, fcolor, bmp.Width, bmp.Height / 2 + 6);

            return CombineBitmaps(bmp, textBmp, true);
        }

        public static Bitmap DrawTextAboveBitmap(Bitmap bmp, string txt, Font font, Color bgcolor, Color fcolor, StringFormat drawFormat)
        {
            Bitmap textBmp = ConvertTextToImage(txt, font, bgcolor, fcolor, bmp.Width, (int)(3.0 * font.Size), drawFormat);

            return CombineBitmaps(textBmp, bmp, true);
        }

        public static Bitmap ConvertTextToImage(string txt, Font font, Color bgcolor, Color fcolor, int width, int height, StringFormat drawFormat)
        {
            Bitmap bmp = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                graphics.FillRectangle(new SolidBrush(bgcolor), 0, 0, bmp.Width, bmp.Height);
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                //StringFormat drawFormat = new StringFormat();
                //drawFormat.Alignment = StringAlignment.Center;

                graphics.DrawString(txt, font, new SolidBrush(fcolor), new RectangleF(0, Math.Max(0, (bmp.Height / 2) - font.Size), bmp.Width, bmp.Height), drawFormat);
                //graphics.DrawString(txt, font, new SolidBrush(fcolor), 0, 0);

                graphics.Flush();
                font.Dispose(); // This is automatically done in a using statement
                graphics.Dispose(); // This is automatically done in a using statement
            }
            return bmp;
        }


        public static Bitmap ConvertTextToImage(string txt, string fontname, int fontsize, Color bgcolor, Color fcolor, int width, int Height)
        {
            Bitmap bmp = new Bitmap(width, Height);
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                Font font = new Font(fontname, fontsize);
                graphics.FillRectangle(new SolidBrush(bgcolor), 0, 0, bmp.Width, bmp.Height);
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                StringFormat drawFormat = new StringFormat();
                drawFormat.Alignment = StringAlignment.Center;

                //graphics.DrawString(txt, font, new SolidBrush(fcolor), new RectangleF(0, bmp.Height / 2, bmp.Width, bmp.Height), drawFormat);
                graphics.DrawString(txt, font, new SolidBrush(fcolor), 0, 0);

                graphics.Flush();
                font.Dispose(); // This is automatically done in a using statement
                graphics.Dispose(); // This is automatically done in a using statement
            }
            return bmp;
        }

        #region Color Manipulation Section

        public static Bitmap ChangeColor(Bitmap bmp, Color inColor, Color outColor)
        {
            return ChangeColor(bmp, inColor.R, inColor.G, inColor.B, outColor.R, outColor.G, outColor.B);
        }

        public static Bitmap ChangeColor(Bitmap bmp, Color inColor, byte outColorR, byte outColorG, byte outColorB)
        {
            return ChangeColor(bmp, inColor.R, inColor.G, inColor.B, outColorR, outColorG, outColorB);
        }

        public static Bitmap ChangeColor(Bitmap bmp, byte inColorR, byte inColorG, byte inColorB, Color outColor)
        {
            return ChangeColor(bmp, inColorR, inColorG, inColorB, outColor.R, outColor.G, outColor.B);
        }

        public static Bitmap ChangeColor(Bitmap bmp, byte inColorR, byte inColorG, byte inColorB, byte outColorR, byte outColorG, byte outColorB)
        {
            // Specify a pixel format.
            PixelFormat pxf = PixelFormat.Format24bppRgb;

            // Lock the bitmap's bits.
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData =
            bmp.LockBits(rect, ImageLockMode.ReadWrite,
                         pxf);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap. 
            // int numBytes = bmp.Width * bmp.Height * 3; 
            int numBytes = bmpData.Stride * bmp.Height;
            byte[] rgbValues = new byte[numBytes];

            // Copy the RGB values into the array.
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            // Manipulate the bitmap
            for (int counter = 0; counter < rgbValues.Length; counter += 3)
            {
                if (rgbValues[counter] == inColorR &&
                    rgbValues[counter + 1] == inColorG &&
                    rgbValues[counter + 2] == inColorB)
                {
                    rgbValues[counter] = outColorR;
                    rgbValues[counter + 1] = outColorG;
                    rgbValues[counter + 2] = outColorB;
                }
            }

            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);

            return bmp;
        }

        public static bool GetKnownColor(int iARGBValue, out string strKnownColor, out Color someColor)
        {
            someColor = Color.Transparent;

            Array aListofKnownColors = Enum.GetValues(typeof(KnownColor));
            foreach (KnownColor eKnownColor in aListofKnownColors)
            {
                someColor = Color.FromKnownColor(eKnownColor);
                if (iARGBValue == someColor.ToArgb() && !someColor.IsSystemColor)
                {
                    strKnownColor = someColor.Name;
                    return true;
                }
            }
            strKnownColor = "";
            return false;
        }

        public static Color ConvertFromNameOrValueToColor(string name)
        {
            Color newColor = Color.FromArgb(0, 0, 0, 0);
            newColor = Color.FromName(name);
            byte g = 0;
            byte b = 0;
            byte r = 0;
            byte a = 0;
            if (newColor.IsKnownColor == true)
            {
                g = newColor.G;
                b = newColor.B;
                r = newColor.R;
                a = newColor.A;
            }
            else
            {
                string[] values = name.Split('_');
                int TotalValues = values.Length;
                if (TotalValues != 4)
                {
                    newColor = Color.FromArgb(0, 0, 0, 0);
                    return newColor;
                }
                else
                {
                    // Parse string for 4 values
                    int alpha = 0, red = 0, blue = 0, green = 0;
                    if (int.TryParse(values[0], out alpha) == true && int.TryParse(values[1], out red) == true && int.TryParse(values[2], out blue) == true && int.TryParse(values[3], out green) == true)
                    {
                        newColor = Color.FromArgb(alpha, red, blue, green);
                        g = newColor.G;
                        b = newColor.B;
                        r = newColor.R;
                        a = newColor.A;
                    }
                    else
                    {
                        newColor = Color.FromArgb(0, 0, 0, 0);
                        return newColor;
                    }
                }
            }
            Color color = newColor;
            string colorName;
            if (GetKnownColor(newColor.ToArgb(), out colorName, out color) == true)
            {
                if (newColor != color)
                    newColor = color;
            }

            return newColor;
        }

        public static bool WriteColorToString(Color color, out string colorString)
        {
            colorString = color.Name;
            int ARGB;
            byte g;
            byte b;
            byte r;
            byte a;
            if (color.IsNamedColor == true)
            {
                colorString = color.Name;
            }
            else if (color.IsKnownColor == true)
            {
                colorString = color.Name;
            }
            else if (color.IsSystemColor == true)
            {
                colorString = color.Name;
            }
            else
            {
                ARGB = color.ToArgb();
                g = color.G;
                b = color.B;
                r = color.R;
                a = color.A;
                colorString = a.ToString() + "_" + r.ToString() + "_" + g.ToString() + "_" + b.ToString();
                if (colorString == "0_0_0_0")
                {
                    int EmptyColorBug = 0;
                    EmptyColorBug += 1;
                    colorString = "255_255_255_255";
                }
            }

            color = ConvertFromNameOrValueToColor(colorString);
            g = color.G;
            b = color.B;
            r = color.R;
            a = color.A;
            if (g == 0 && b == 0 && r == 0 && a == 0)
            {
                color = Color.White;
                return false;
            }
            return true;
        }

        public static bool ReadColorFromString(string ColorString, out Color color)
        {
            color = Color.White;
            try
            {
                color = ConvertFromNameOrValueToColor(ColorString);
            }
            catch
            {
                return false;
            }
            return true;
        }

        #endregion Color Manipulation Section

        //public static Bitmap ClipAndScale(Bitmap bmap, int xMin, int yMin, float scaleX, float scaleY)
        //{
        //    Matrix matrix = new Matrix();
        //    matrix.Scale(scaleX, scaleY);
        //    matrix.Translate(xMin, yMin);

        //    Graphics g = new Graphics();
        //    g.DrawImage(bmap,
        //    destination rectangle (usually {0, 0, width, height}),
        //    source rectangle (the desired area from the original image),
        //    GraphicsUnit.Pixel);

        //    BitmapData newBMapData = new BitmapData(bmap.Width, bmap.Height);
        //    newBMap.draw(bMapData, matrix, null, null, null, true);
        //}

        public static System.Drawing.Bitmap CombineBitmaps(List<System.Drawing.Bitmap> images, bool combineVertically = true)
        {
            System.Drawing.Bitmap finalImage = null;

            try
            {
                int width = 0;
                int height = 0;

                if (combineVertically == true)
                {
                    foreach (Bitmap bitmap in images)
                    {
                        //update the size of the final bitmap
                        height += bitmap.Height;
                        width = bitmap.Width > width ? bitmap.Width : width;
                    }
                }
                else // Horizontally
                {
                    foreach (Bitmap bitmap in images)
                    {
                        //update the size of the final bitmap
                        width += bitmap.Width;
                        height = bitmap.Height > height ? bitmap.Height : height;
                    }
                }

                //create a bitmap to hold the combined image
                finalImage = new System.Drawing.Bitmap(width, height);

                //get a graphics object from the image so we can draw on it
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(finalImage))
                {
                    //set background color
                    g.Clear(System.Drawing.Color.Black);

                    //go through each image and draw it on the final image
                    int offset = 0;
                    if (combineVertically == true)
                    {
                        foreach (System.Drawing.Bitmap image in images)
                        {
                            g.DrawImage(image,
                              new System.Drawing.Rectangle(0, offset, image.Width, image.Height));
                            offset += image.Height;
                        }
                    }
                    else
                    {
                        foreach (System.Drawing.Bitmap image in images)
                        {
                            g.DrawImage(image,
                              new System.Drawing.Rectangle(offset, 0, image.Width, image.Height));
                            offset += image.Width;
                        }
                    }
                }

                return finalImage;
            }
            catch (Exception ex)
            {
                if (finalImage != null)
                    finalImage.Dispose();

                throw ex;
            }
            finally
            {
                //clean up memory
                foreach (System.Drawing.Bitmap image in images)
                {
                    image.Dispose();
                }
            }
        }

        public static System.Drawing.Bitmap CombineBitmaps(string[] files, bool combineVertically = true)
        {
            //read all images into memory
            List<System.Drawing.Bitmap> images = new List<System.Drawing.Bitmap>();

            try
            {
                foreach (string image in files)
                {
                    //create a Bitmap from the file and add it to the list
                    System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(image);
                    images.Add(bitmap);
                }

                return CombineBitmaps(images, combineVertically);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //clean up memory
                foreach (System.Drawing.Bitmap image in images)
                {
                    image.Dispose();
                }
            }
        }

        public static Bitmap MakeGrayscale1(Bitmap original) // slow!!!
        {
            //make an empty bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    Color originalColor = original.GetPixel(i, j);

                    //create the grayscale version of the pixel
                    int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59)
                        + (originalColor.B * .11));

                    //create the color object
                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, newColor);
                }
            }

            return newBitmap;
        }
        
        public static Bitmap MakeGrayscale2(Bitmap c) // faster
        {
            Bitmap d = new Bitmap(c.Width, c.Height);

            //LockBitmap lockBitmap = new LockBitmap(d);
            //lockBitmap.LockBits();

            for (int i = 0; i < c.Width; i++)
            {
                for (int x = 0; x < c.Height; x++)
                {
                    Color oc = c.GetPixel(i, x);
                    int grayScale = (int)((oc.R * 0.3) + (oc.G * 0.59) + (oc.B * 0.11));
                    Color nc = Color.FromArgb(oc.A, grayScale, grayScale, grayScale);
                    d.SetPixel(i, x, nc);
                }
            }
            //lockBitmap.UnlockBits();

            return d;
        }

        public static Bitmap MakeGrayscale3(Bitmap original) // fastest!!!
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][] 
      {
         new float[] {.3f, .3f, .3f, 0, 0},
         new float[] {.59f, .59f, .59f, 0, 0},
         new float[] {.11f, .11f, .11f, 0, 0},
         new float[] {0, 0, 0, 1, 0},
         new float[] {0, 0, 0, 0, 1}
      });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }

        public static Bitmap CombineBitmaps(Bitmap bmp1, Bitmap bmp2, bool combineVertically = true)
        {
            int width = 0;
            int height = 0;

            List<Bitmap> images = new List<Bitmap>();
            Bitmap finalImage = null;

            try
            {
                if (combineVertically == true)
                {
                    height = bmp1.Height + bmp2.Height;
                    width = bmp1.Width > width ? bmp2.Width : width;
                }
                else
                {
                    width = bmp1.Width + bmp2.Width;
                    height = bmp1.Height > height ? bmp2.Height : height;
                }
                images.Add(bmp1);
                images.Add(bmp2);

                //create a bitmap to hold the combined image
                finalImage = new System.Drawing.Bitmap(width, height);

                //get a graphics object from the image so we can draw on it
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(finalImage))
                {
                    //set background color
                    g.Clear(System.Drawing.Color.Black);

                    //go through each image and draw it on the final image
                    int offset = 0;
                    if (combineVertically == true)
                    {
                        foreach (System.Drawing.Bitmap image in images)
                        {
                            g.DrawImage(image,
                              new System.Drawing.Rectangle(0, offset, image.Width, image.Height));
                            offset += image.Height;
                        }
                    }
                    else
                    {
                        foreach (System.Drawing.Bitmap image in images)
                        {
                            g.DrawImage(image,
                              new System.Drawing.Rectangle(offset, 0, image.Width, image.Height));
                            offset += image.Width;
                        }
                    }
                }

                return finalImage;
            }
            catch (Exception ex)
            {
                if (finalImage != null)
                    finalImage.Dispose();

                throw ex;
            }
            finally
            {
                //clean up memory
                foreach (System.Drawing.Bitmap image in images)
                {
                    image.Dispose();
                }
            }
        }

        public static void WriStr(FileStream Out, string s)
        {
            Out.Write(System.Text.Encoding.ASCII.GetBytes(s), 0, s.Length);
        }

        public static void JPEGToPDF(string InJpg, string OutPdf)
        {
            var stream = File.OpenRead(InJpg); // The easiest way to get the metadata is to temporaryly load it as a BMP
            Bitmap bmp = (Bitmap)Bitmap.FromStream(stream);
            BitmapToPDF(bmp, OutPdf);
            stream.Close();
        }

        public static void BitmapListToPDF(List<Bitmap> images, string OutPdf, bool combineVertically = true)
        {
            Bitmap bmap = CombineBitmaps(images, combineVertically);
            BitmapToPDF(bmap, OutPdf);
        }

        public static void BitmapToPDF(Bitmap bmp, string OutPdf)
        {
            try
            {
                //string InJpg = @"InFile.JPG";
                //string OutPdf = @"OutFile.pdf";

                int w = bmp.Width; String wf = (w * 72 / bmp.HorizontalResolution).ToString().Replace(",", ".");
                int h = bmp.Height; ; string hf = (h * 72 / bmp.VerticalResolution).ToString().Replace(",", ".");

                FileStream Out = File.Create(OutPdf);

                var lens = new List<long>();

                WriStr(Out, "%PDF-1.5\r\n");

                lens.Add(Out.Position);
                WriStr(Out, lens.Count.ToString() + " 0 obj " + "<</Type /Catalog\r\n/Pages 2 0 R>>\r\nendobj\r\n");

                lens.Add(Out.Position);
                WriStr(Out, lens.Count.ToString() + " 0 obj " + "<</Count 1/Kids [ <<\r\n" +
                            "/Type /Page\r\n" +
                            "/Parent 2 0 R\r\n" +
                            "/MediaBox [0 0 " + wf + " " + hf + "]\r\n" +
                            "/Resources<<  /ProcSet [/PDF /ImageC]\r\n /XObject <</Im1 4 0 R >>  >>\r\n" +
                            "/Contents 3 0 R\r\n" +
                            ">>\r\n ]\r\n" +
                            ">>\r\nendobj\r\n");

                string X = "\r\n" +
                    "q\r\n" +
                    "" + wf + " 0 0 " + hf + " 0 0 cm\r\n" +
                    "/Im1 Do\r\n" +
                    "Q\r\n";
                lens.Add(Out.Position);
                WriStr(Out, lens.Count.ToString() + " 0 obj " + "<</Length " + X.Length.ToString() + ">>" +
                            "stream" + X + "endstream\r\n" +
                            "endobj\r\n");
                lens.Add(Out.Position);
                WriStr(Out, lens.Count.ToString() + " 0 obj " + "<</Name /Im1" +
                            "/Type /XObject\r\n" +
                            "/Subtype /Image\r\n" +
                            "/Width " + w.ToString() +
                            "/Height " + h.ToString() +
                            "/Length 5 0 R\r\n" +
                            "/Filter /DCTDecode\r\n" +
                            "/ColorSpace /DeviceRGB\r\n" +
                            "/BitsPerComponent 8\r\n" +
                            ">> stream\r\n");
                long Siz = Out.Position;

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                Out.Write(ms.ToArray(), 0, (int)ms.Length);
                
                //var in1 = File.OpenRead(InJpg);
                //while (true)
                //{
                //    var len = in1.Read(buffer, 0, buffer.Length);
                //    if (len != 0) Out.Write(buffer, 0, len); else break;
                //}
                //in1.Close();

                Siz = Out.Position - Siz;
                WriStr(Out, "\r\nendstream\r\n" +
                            "endobj\r\n");

                lens.Add(Out.Position);
                WriStr(Out, lens.Count.ToString() + " 0 obj " + Siz.ToString() + " endobj\r\n");

                long startxref = Out.Position;

                WriStr(Out, "xref\r\n" +
                            "0 " + (lens.Count + 1).ToString() + "\r\n" +
                            "0000000000 65535 f\r\n");
                foreach (var L in lens)
                    WriStr(Out, (10000000000 + L).ToString().Substring(1) + " 00000 n\r\n");
                WriStr(Out, "trailer\r\n" +
                            "<<\r\n" +
                            "  /Size " + (lens.Count + 1).ToString() + "\r\n" +
                            "  /Root 1 0 R\r\n" +
                            ">>\r\n" +
                            "startxref\r\n" +
                            startxref.ToString() + "\r\n%%EOF");
                Out.Close();
            }
            catch
            {
            }
        }

        public static void DisposeBitmap(Bitmap bmp)
        {
            //if (bmp != null)
            //    bmp.Dispose();
        }
        
        public static void DisposeBitmap(Image bmp)
        {
            //if (bmp != null)
            //    bmp.Dispose();
        }

        [DllImport("msvcrt.dll")]
        private static extern int memcmp(IntPtr b1, IntPtr b2, long count);

        public static bool CompareMemCmp(Bitmap b1, Bitmap b2)
        {
            if ((b1 == null) != (b2 == null)) return false;
            if (b1.Size != b2.Size) return false;

            var bd1 = b1.LockBits(new Rectangle(new Point(0, 0), b1.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bd2 = b2.LockBits(new Rectangle(new Point(0, 0), b2.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                IntPtr bd1scan0 = bd1.Scan0;
                IntPtr bd2scan0 = bd2.Scan0;

                int stride = bd1.Stride;
                int len = stride * b1.Height;

                return memcmp(bd1scan0, bd2scan0, len) == 0;
            }
            finally
            {
                b1.UnlockBits(bd1);
                b2.UnlockBits(bd2);
            }
        }
        
        public static bool CompareBitmaps(Bitmap b1, Bitmap b2)
        {
            if ((b1 == null) != (b2 == null)) return false;
            if (b1.Size != b2.Size) return false;

            var bd1 = b1.LockBits(new Rectangle(new Point(0, 0), b1.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bd2 = b2.LockBits(new Rectangle(new Point(0, 0), b2.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                IntPtr bd1scan0 = bd1.Scan0;
                IntPtr bd2scan0 = bd2.Scan0;

                int stride = bd1.Stride;
                int len = stride * b1.Height;

                return memcmp(bd1scan0, bd2scan0, len) == 0;
            }
            finally
            {
                b1.UnlockBits(bd1);
                b2.UnlockBits(bd2);
            }
        }

        #region Bitmap Image Processing

        public static Bitmap ApplyInvert(Bitmap bmap)
        {
            Bitmap bitmapImage = (Bitmap)bmap.Clone();

            //LockBitmap lockBitmap = new LockBitmap(bitmapImage);
            //lockBitmap.LockBits();

            byte A, R, G, B;
            Color pixelColor;

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = pixelColor.A;
                    R = (byte)(255 - pixelColor.R);
                    G = (byte)(255 - pixelColor.G);
                    B = (byte)(255 - pixelColor.B);
                    bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }
            
            //lockBitmap.UnlockBits();

            return bitmapImage;
        }

        public static Bitmap ApplyGreyscale(Bitmap bmap)
        {
            byte A, R, G, B;
            Color pixelColor;

            Bitmap bitmapImage = (Bitmap)bmap.Clone();

            //LockBitmap lockBitmap = new LockBitmap(bitmapImage);
            //lockBitmap.LockBits();

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = pixelColor.A;
                    R = (byte)((0.299 * pixelColor.R) + (0.587 * pixelColor.G) + (0.114 * pixelColor.B));
                    G = B = R;

                    bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }
            
            //lockBitmap.UnlockBits();

            return bitmapImage;
        }

        public static Bitmap ApplyGamma(Bitmap bmap, double r, double g, double b)
        {
            byte A, R, G, B;
            Color pixelColor;

            Bitmap bitmapImage = (Bitmap)bmap.Clone();

            //LockBitmap lockBitmap = new LockBitmap(bitmapImage);
            //lockBitmap.LockBits();

            byte[] redGamma = new byte[256];
            byte[] greenGamma = new byte[256];
            byte[] blueGamma = new byte[256];

            for (int i = 0; i < 256; ++i)
            {
                redGamma[i] = (byte)Math.Min(255, (int)((255.0
                    * Math.Pow(i / 255.0, 1.0 / r)) + 0.5));
                greenGamma[i] = (byte)Math.Min(255, (int)((255.0
                    * Math.Pow(i / 255.0, 1.0 / g)) + 0.5));
                blueGamma[i] = (byte)Math.Min(255, (int)((255.0
                    * Math.Pow(i / 255.0, 1.0 / b)) + 0.5));
            }

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = pixelColor.A;
                    R = redGamma[pixelColor.R];
                    G = greenGamma[pixelColor.G];
                    B = blueGamma[pixelColor.B];
                    bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }
            
            //lockBitmap.UnlockBits();

            return bitmapImage;
        }

        public static Bitmap ApplyColorFilter(Bitmap bmap, double r, double g, double b)
        {
            byte A, R, G, B;
            Color pixelColor;

            Bitmap bitmapImage = (Bitmap)bmap.Clone();

            //LockBitmap lockBitmap = new LockBitmap(bitmapImage);
            //lockBitmap.LockBits();

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = pixelColor.A;
                    R = (byte)(pixelColor.R * r);
                    G = (byte)(pixelColor.G * g);
                    B = (byte)(pixelColor.B * b);
                    bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }
            
            //lockBitmap.UnlockBits();

            return bitmapImage;
        }

        public static Bitmap ApplySepia(Bitmap bmap, int depth)
        {
            int A, R, G, B;
            Color pixelColor;

            Bitmap bitmapImage = (Bitmap)bmap.Clone();

            //LockBitmap lockBitmap = new LockBitmap(bitmapImage);
            //lockBitmap.LockBits();

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = pixelColor.A;
                    R = (int)((0.299 * pixelColor.R) + (0.587 * pixelColor.G) + (0.114 * pixelColor.B));
                    G = B = R;

                    R += (depth * 2);
                    if (R > 255)
                    {
                        R = 255;
                    }
                    G += depth;
                    if (G > 255)
                    {
                        G = 255;
                    }

                    bitmapImage.SetPixel(x, y, Color.FromArgb(A, R, G, B));
                }
            }
            
            //lockBitmap.UnlockBits();

            return bitmapImage;
        }

        public static Bitmap ApplyDecreaseColourDepth(Bitmap bmap, int offset)
        {
            int A, R, G, B;
            Color pixelColor;

            Bitmap bitmapImage = (Bitmap)bmap.Clone();

            //LockBitmap lockBitmap = new LockBitmap(bitmapImage);
            //lockBitmap.LockBits();

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = pixelColor.A;
                    R = ((pixelColor.R + (offset / 2)) - ((pixelColor.R + (offset / 2)) % offset) - 1);
                    if (R < 0)
                    {
                        R = 0;
                    }
                    G = ((pixelColor.G + (offset / 2)) - ((pixelColor.G + (offset / 2)) % offset) - 1);
                    if (G < 0)
                    {
                        G = 0;
                    }
                    B = ((pixelColor.B + (offset / 2)) - ((pixelColor.B + (offset / 2)) % offset) - 1);
                    if (B < 0)
                    {
                        B = 0;
                    }
                    bitmapImage.SetPixel(x, y, Color.FromArgb(A, R, G, B));
                }
            }

            //lockBitmap.UnlockBits();

            return bitmapImage;
        }

        public static Bitmap ApplyContrast(Bitmap bmap, double contrast)
        {
            double A, R, G, B;
            Color pixelColor;

            Bitmap bitmapImage = (Bitmap)bmap.Clone();

            //LockBitmap lockBitmap = new LockBitmap(bitmapImage);
            //lockBitmap.LockBits();

            contrast = (100.0 + contrast) / 100.0;
            contrast *= contrast;

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = pixelColor.A;

                    R = pixelColor.R / 255.0;
                    R -= 0.5;
                    R *= contrast;
                    R += 0.5;
                    R *= 255;

                    if (R > 255)
                    {
                        R = 255;
                    }
                    else if (R < 0)
                    {
                        R = 0;
                    }

                    G = pixelColor.G / 255.0;
                    G -= 0.5;
                    G *= contrast;
                    G += 0.5;
                    G *= 255;
                    if (G > 255)
                    {
                        G = 255;
                    }
                    else if (G < 0)
                    {
                        G = 0;
                    }

                    B = pixelColor.B / 255.0;
                    B -= 0.5;
                    B *= contrast;
                    B += 0.5;
                    B *= 255;
                    if (B > 255)
                    {
                        B = 255;
                    }
                    else if (B < 0)
                    {
                        B = 0;
                    }

                    bitmapImage.SetPixel(x, y, Color.FromArgb((int)A, (int)R, (int)G, (int)B));
                }
            }
            
            //lockBitmap.UnlockBits();

            return bitmapImage;
        }

        public static Bitmap ApplyBrightness(Bitmap bmap, int brightness)
        {
            int A, R, G, B;
            Color pixelColor;

            Bitmap bitmapImage = (Bitmap)bmap.Clone();

            //LockBitmap lockBitmap = new LockBitmap(bitmapImage);
            //lockBitmap.LockBits();

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = pixelColor.A;
                    R = pixelColor.R + brightness;
                    if (R > 255)
                    {
                        R = 255;
                    }
                    else if (R < 0)
                    {
                        R = 0;
                    }

                    G = pixelColor.G + brightness;
                    if (G > 255)
                    {
                        G = 255;
                    }
                    else if (G < 0)
                    {
                        G = 0;
                    }

                    B = pixelColor.B + brightness;
                    if (B > 255)
                    {
                        B = 255;
                    }
                    else if (B < 0)
                    {
                        B = 0;
                    }

                    bitmapImage.SetPixel(x, y, Color.FromArgb(A, R, G, B));
                }
            }
            
            //lockBitmap.UnlockBits();

            return bitmapImage;
        }

        public static Bitmap ApplySmooth(Bitmap bitmapImage, double weight)
        {
            ConvolutionMatrix matrix = new ConvolutionMatrix(3);
            matrix.SetAll(1);
            matrix.Matrix[1, 1] = weight;
            matrix.Factor = weight + 8;
            return Convolution3x3(bitmapImage, matrix);
        }

        public static Bitmap ApplyFirstDerivativeColor(Bitmap image)
        {
            Bitmap ret = new Bitmap(image.Width, image.Height);
            for (int i = 1; i < image.Width - 1; i++)
            {
                for (int j = 1; j < image.Height - 1; j++)
                {
                    Color cr = image.GetPixel(i + 1, j);
                    Color cl = image.GetPixel(i - 1, j);
                    Color cu = image.GetPixel(i, j - 1);
                    Color cd = image.GetPixel(i, j + 1);
                    int dx = (int)((cr.R - cl.R) * 0.3 + (cr.G - cl.G) * 0.59 + (cr.B - cl.B) * 0.11);
                    int dy = (int)((cd.R - cu.R) * 0.3 + (cd.G - cu.G) * 0.59 + (cd.B - cu.B) * 0.11);
                    double power = Math.Sqrt(dx * dx / 4 + dy * dy / 4);
                    if (power > 14)
                        ret.SetPixel(i, j, Color.Yellow);
                    else
                        ret.SetPixel(i, j, Color.Black);
                }
            }
            return ret;
        }

        public static Bitmap ApplyFirstDerivativeGreyScale(Bitmap image)
        {
            Bitmap ret = new Bitmap(image.Width, image.Height);
            for (int i = 1; i < image.Width - 1; i++)
            {
                for (int j = 1; j < image.Height - 1; j++)
                {
                    Color cr = image.GetPixel(i + 1, j);
                    Color cl = image.GetPixel(i - 1, j);
                    Color cu = image.GetPixel(i, j - 1);
                    Color cd = image.GetPixel(i, j + 1);
                    int dx = cr.R - cl.R;
                    int dy = cd.R - cu.R;
                    double power = Math.Sqrt(dx * dx / 4 + dy * dy / 4);
                    if (power > 14)
                        ret.SetPixel(i, j, Color.Yellow);
                    else
                        ret.SetPixel(i, j, Color.Black);
                }
            }
            return ret;
        }

        public static Bitmap ApplyGaussianBlur(Bitmap bitmapImage, double peakValue)
        {
            ConvolutionMatrix matrix = new ConvolutionMatrix(3);
            matrix.SetAll(1);
            matrix.Matrix[0, 0] = peakValue / 4;
            matrix.Matrix[1, 0] = peakValue / 2;
            matrix.Matrix[2, 0] = peakValue / 4;
            matrix.Matrix[0, 1] = peakValue / 2;
            matrix.Matrix[1, 1] = peakValue;
            matrix.Matrix[2, 1] = peakValue / 2;
            matrix.Matrix[0, 2] = peakValue / 4;
            matrix.Matrix[1, 2] = peakValue / 2;
            matrix.Matrix[2, 2] = peakValue / 4;
            matrix.Factor = peakValue * 4;
            return Convolution3x3(bitmapImage, matrix);
        }

        public static Bitmap ApplySharpen(Bitmap bitmapImage, double weight)
        {
            ConvolutionMatrix matrix = new ConvolutionMatrix(3);
            matrix.SetAll(1);
            matrix.Matrix[0, 0] = 0;
            matrix.Matrix[1, 0] = -2;
            matrix.Matrix[2, 0] = 0;
            matrix.Matrix[0, 1] = -2;
            matrix.Matrix[1, 1] = weight;
            matrix.Matrix[2, 1] = -2;
            matrix.Matrix[0, 2] = 0;
            matrix.Matrix[1, 2] = -2;
            matrix.Matrix[2, 2] = 0;
            matrix.Factor = weight - 8;
            return Convolution3x3(bitmapImage, matrix);
        }

        public static Bitmap ApplyMeanRemoval(Bitmap bitmapImage, double weight)
        {
            ConvolutionMatrix matrix = new ConvolutionMatrix(3);
            matrix.SetAll(1);
            matrix.Matrix[0, 0] = -1;
            matrix.Matrix[1, 0] = -1;
            matrix.Matrix[2, 0] = -1;
            matrix.Matrix[0, 1] = -1;
            matrix.Matrix[1, 1] = weight;
            matrix.Matrix[2, 1] = -1;
            matrix.Matrix[0, 2] = -1;
            matrix.Matrix[1, 2] = -1;
            matrix.Matrix[2, 2] = -1;
            matrix.Factor = weight - 8;
            return Convolution3x3(bitmapImage, matrix);
        }

        public static Bitmap ApplyEmboss(Bitmap bitmapImage, double weight)
        {
            ConvolutionMatrix matrix = new ConvolutionMatrix(3);
            matrix.SetAll(1);
            matrix.Matrix[0, 0] = -1;
            matrix.Matrix[1, 0] = 0;
            matrix.Matrix[2, 0] = -1;
            matrix.Matrix[0, 1] = 0;
            matrix.Matrix[1, 1] = weight;
            matrix.Matrix[2, 1] = 0;
            matrix.Matrix[0, 2] = -1;
            matrix.Matrix[1, 2] = 0;
            matrix.Matrix[2, 2] = -1;
            matrix.Factor = 4;
            matrix.Offset = 127;
            return Convolution3x3(bitmapImage, matrix);
        }

        public static Bitmap Convolution3x3(Bitmap b, ConvolutionMatrix m)
        {
            Bitmap newImg = (Bitmap)b.Clone();

            //LockBitmap lockBitmap = new LockBitmap(newImg);
            //lockBitmap.LockBits();

            Color[,] pixelColor = new Color[3, 3];
            int A, R, G, B;

            for (int y = 0; y < b.Height - 2; y++)
            {
                for (int x = 0; x < b.Width - 2; x++)
                {
                    pixelColor[0, 0] = b.GetPixel(x, y);
                    pixelColor[0, 1] = b.GetPixel(x, y + 1);
                    pixelColor[0, 2] = b.GetPixel(x, y + 2);
                    pixelColor[1, 0] = b.GetPixel(x + 1, y);
                    pixelColor[1, 1] = b.GetPixel(x + 1, y + 1);
                    pixelColor[1, 2] = b.GetPixel(x + 1, y + 2);
                    pixelColor[2, 0] = b.GetPixel(x + 2, y);
                    pixelColor[2, 1] = b.GetPixel(x + 2, y + 1);
                    pixelColor[2, 2] = b.GetPixel(x + 2, y + 2);

                    A = pixelColor[1, 1].A;

                    R = (int)((((pixelColor[0, 0].R * m.Matrix[0, 0]) +
                                 (pixelColor[1, 0].R * m.Matrix[1, 0]) +
                                 (pixelColor[2, 0].R * m.Matrix[2, 0]) +
                                 (pixelColor[0, 1].R * m.Matrix[0, 1]) +
                                 (pixelColor[1, 1].R * m.Matrix[1, 1]) +
                                 (pixelColor[2, 1].R * m.Matrix[2, 1]) +
                                 (pixelColor[0, 2].R * m.Matrix[0, 2]) +
                                 (pixelColor[1, 2].R * m.Matrix[1, 2]) +
                                 (pixelColor[2, 2].R * m.Matrix[2, 2]))
                                        / m.Factor) + m.Offset);

                    if (R < 0)
                    {
                        R = 0;
                    }
                    else if (R > 255)
                    {
                        R = 255;
                    }

                    G = (int)((((pixelColor[0, 0].G * m.Matrix[0, 0]) +
                                 (pixelColor[1, 0].G * m.Matrix[1, 0]) +
                                 (pixelColor[2, 0].G * m.Matrix[2, 0]) +
                                 (pixelColor[0, 1].G * m.Matrix[0, 1]) +
                                 (pixelColor[1, 1].G * m.Matrix[1, 1]) +
                                 (pixelColor[2, 1].G * m.Matrix[2, 1]) +
                                 (pixelColor[0, 2].G * m.Matrix[0, 2]) +
                                 (pixelColor[1, 2].G * m.Matrix[1, 2]) +
                                 (pixelColor[2, 2].G * m.Matrix[2, 2]))
                                        / m.Factor) + m.Offset);

                    if (G < 0)
                    {
                        G = 0;
                    }
                    else if (G > 255)
                    {
                        G = 255;
                    }

                    B = (int)((((pixelColor[0, 0].B * m.Matrix[0, 0]) +
                                 (pixelColor[1, 0].B * m.Matrix[1, 0]) +
                                 (pixelColor[2, 0].B * m.Matrix[2, 0]) +
                                 (pixelColor[0, 1].B * m.Matrix[0, 1]) +
                                 (pixelColor[1, 1].B * m.Matrix[1, 1]) +
                                 (pixelColor[2, 1].B * m.Matrix[2, 1]) +
                                 (pixelColor[0, 2].B * m.Matrix[0, 2]) +
                                 (pixelColor[1, 2].B * m.Matrix[1, 2]) +
                                 (pixelColor[2, 2].B * m.Matrix[2, 2]))
                                        / m.Factor) + m.Offset);

                    if (B < 0)
                    {
                        B = 0;
                    }
                    else if (B > 255)
                    {
                        B = 255;
                    }
                    newImg.SetPixel(x + 1, y + 1, Color.FromArgb(A, R, G, B));
                }
            }

            //lockBitmap.UnlockBits();

            return newImg;
        }

        public enum CannyImageDisplayType
        {
            NonMax,
            FilteredImage,
            GNL,
            GNH,
            EdgeMap
        };

        public static Bitmap ApplyCanny(Bitmap bmap, float hiThreshold, float loThreshold, int maskSize, float sigma, CannyImageDisplayType cannyImageDisplayType)
        {
            Canny CannyData;
            float TH, TL, Sigma;
            int MaskSize;

            TH = hiThreshold; // 20.0f;
            TL = loThreshold; // 10.0f;

            MaskSize = maskSize; // 5; must be odd and >= 3
            Sigma = sigma; // 1.0f;
            CannyData = new Canny(bmap, TH, TL, MaskSize, Sigma);

            if (cannyImageDisplayType == CannyImageDisplayType.NonMax)
            {
                return CannyData.DisplayImage(CannyData.NonMax);
            }
            else if (cannyImageDisplayType == CannyImageDisplayType.FilteredImage)
            {
                return CannyData.DisplayImage(CannyData.FilteredImage);
            }
            else if (cannyImageDisplayType == CannyImageDisplayType.GNL)
            {
                return CannyData.DisplayImage(CannyData.GNL);
            }
            else if (cannyImageDisplayType == CannyImageDisplayType.GNH)
            {
                return CannyData.DisplayImage(CannyData.GNH);
            }
            else // if (cannyImageDisplayType == CannyImageDisplayType.EdgeMap)
            {
                return CannyData.DisplayImage(CannyData.EdgeMap);
            }
        }

        public static Bitmap ApplyMaxIntensityRange(Bitmap bmap)
        {
            int A, R, G, B;
            Color pixelColor;

            Bitmap bitmapImage = (Bitmap)bmap.Clone();

            //LockBitmap lockBitmap = new LockBitmap(bitmapImage);
            //lockBitmap.LockBits();

            int min = 255, max = 0;
            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    R = (byte)((0.299 * pixelColor.R) + (0.587 * pixelColor.G) + (0.114 * pixelColor.B));
                    max = Math.Max(pixelColor.R, max);
                    min = Math.Min(pixelColor.R, min);
                }
            }

            for (int y = 0; y < bitmapImage.Height; y++)
            {
                for (int x = 0; x < bitmapImage.Width; x++)
                {
                    pixelColor = bitmapImage.GetPixel(x, y);
                    A = pixelColor.A;
                    R = 255 * (pixelColor.R + min) / max;
                    if (R > 255)
                    {
                        R = 255;
                    }
                    else if (R < 0)
                    {
                        R = 0;
                    }

                    G = 255 * (pixelColor.G + min) / max;
                    if (G > 255)
                    {
                        G = 255;
                    }
                    else if (G < 0)
                    {
                        G = 0;
                    }

                    B = 255 * (pixelColor.B + min) / max;
                    if (B > 255)
                    {
                        B = 255;
                    }
                    else if (B < 0)
                    {
                        B = 0;
                    }

                    bitmapImage.SetPixel(x, y, Color.FromArgb(A, R, G, B));
                }
            }
            //lockBitmap.UnlockBits();

            return bitmapImage;
        }

        #endregion Bitmap Image Processing

        //public static Bitmap ApplyLaplacianOfGaussianFilter(Bitmap bmap)
        //{
        //    Bitmap ret = new Bitmap(image.Width, image.Height);
        //    double[,] matrix1 = new double[image.Width, image.Height];
        //    double[,] matrix2 = new double[image.Width, image.Height];

        //    for (int i = 0; i < image.Width; i++)
        //    {
        //        for (int j = 0; j < image.Height; j++)
        //        {
        //            matrix1[i, j] = grayscale(image.GetPixel(i, j)).R;
        //            matrix2[i, j] = grayscale(image.GetPixel(i, j)).R;
        //        }
        //    }
        //    matrix1 = Gaussian.LoGConvolution(matrix1, 1.2);
        //    matrix2 = Gaussian.LoGConvolution(matrix2, 2.8);

        //    for (int i = 0; i < image.Width; i++)
        //    {
        //        for (int j = 0; j < image.Height; j++)
        //        {
        //            int scale = matrix1[i, j] > 0 && matrix2[i, j] > 0 ? 255 : 0;
        //            ret.SetPixel(i, j, Color.FromArgb(255, scale, scale, scale));
        //        }
        //    }
        //    return ret;
        //}

        public static double[,] Calculate2DLoGSampleKernel(double deviation, int size)
        {
            double[,] ret = new double[size, size];
            int half = size / 2;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    double r = Math.Sqrt((i - half) * (i - half) + (j - half) * (j - half));
                    ret[i, j] = ((r * r - 2 * deviation * deviation) / Math.Pow(deviation, 4)) * Math.Exp(-r * r / (2 * deviation * deviation));
                }
            }
            return ret;
        }

        private static double[,] zeroCrossing(double[,] matrix)
        {
            double[,] res = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 1; i < matrix.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < matrix.GetLength(1) - 1; j++)
                {
                    res[i, j] = 0;
                    //left-right
                    if (matrix[i, j - 1] * matrix[i, j + 1] < 0)
                    {
                        res[i, j] = 1;
                        continue;
                    }
                    //top-down
                    if (matrix[i - 1, j] * matrix[i + 1, j] < 0)
                    {
                        res[i, j] = 1;
                        continue;
                    }
                    //upperleft-downright
                    if (matrix[i - 1, j - 1] * matrix[i + 1, j + 1] < 0)
                    {
                        res[i, j] = 1;
                        continue;
                    }
                    //upperright-downleft
                    if (matrix[i - 1, j + 1] * matrix[i + 1, j - 1] < 0)
                    {
                        res[i, j] = 1;
                        continue;
                    }
                }
            }
            return res;
        }
    }

    public class ConvolutionMatrix
        {
            public int MatrixSize = 3;

            public double[,] Matrix;
            public double Factor = 1;
            public double Offset = 1;

            public ConvolutionMatrix(int size)
            {
                MatrixSize = 3;
                Matrix = new double[size, size];
            }

            public void SetAll(double value)
            {
                for (int i = 0; i < MatrixSize; i++)
                {
                    for (int j = 0; j < MatrixSize; j++)
                    {
                        Matrix[i, j] = value;
                    }
                }
            }
        }

}
