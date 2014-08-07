using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRobotKinect
{
    public enum NGEChannel
    {
        Xposition,
        Yposition,
        Zposition,
        Xrotation,
        Yrotation,
        Zrotation
    }

    public enum TransAxis
    {
        None,
        X,
        Y,
        Z,
        nX,
        nY,
        nZ
    }

    public class NGESkeleton
    {
        List<NGEBone> bones;
        int maxDepth = 0;
        int nrBones;
        int channels;

        public List<NGEBone> Bones
        {
            get { return bones; }
        }

        public int Channels
        {
            get { return channels; }
        }

        public NGESkeleton()
        {
            bones = new List<NGEBone>();
        }

        public void AddBone(NGEBone Bone)
        {
            if (!Bones.Contains(Bone))
            {
                bones.Add(Bone);
            }
        }

        public void FinalizeNGESkeleton()
        {
            for (int k = 0; k < Bones.Count(); k++)
            {
                // set max Depth
                if (Bones[k].Depth > maxDepth) 
                    maxDepth = Bones[k].Depth;

                //set Bone Index for Motion Values Array
                int motionCount = 0;
                for (int n = 0; n < k; n++)
                {
                    motionCount += Bones[n].ChannelCount; 
                }
                Bones[k].MotionSpace = motionCount;

                //set Count of Channels for Skeleton
                channels += Bones[k].ChannelCount;

                //set Children
                List<NGEBone> childBoneList = Bones.FindAll(i => i.Parent == Bones[k]);
                if (childBoneList.Count == 0)
                {
                    Bones[k].End = true;
                }
                else
                {
                    Bones[k].Children = childBoneList;

                }
            }
        }

        public void copyParameters(NGESkeleton input)
        {
            channels = input.Channels;
            maxDepth = input.maxDepth;
            nrBones = input.nrBones;
        }

        public int getMaxDepth()
        {
            return maxDepth;
        }
    }

    public class NGEBone
    {
        NGEBone parent;
        List<NGEBone> children;
        string name;
        int depth;
        static int index = 1;
        NGEChannel[] channels;
        public double[] rotOffset = new double[] { 0, 0, 0 };
        public double[] translOffset = new double[]{0, 0, 0};
        bool end;
        bool root;
        int motionSpace; // gibt erstes Element in der Motionspalte an
        TransAxis axis;
        bool isKinectJoint;


        public List<NGEBone> Children
        {
            get { return children; }
            set { children = value; }
        }

        public bool IsKinectJoint
        {
            get { return isKinectJoint; }
            set { isKinectJoint = value; }
        }

        public bool Root
        {
            get { return root; }
            set { root = value; }
        }

        public bool End
        {
            get { return end; }
            set { end = value; }
        }

        public TransAxis Axis
        {
            get { return axis; }
            set { axis = value; }
        }

        public int MotionSpace
        {
            get { return motionSpace; }
            set { motionSpace = value; }
        }

        public int Depth
        {
            get { return depth; }
        }
        public int ChannelCount
        {
            get { return channels.Length; }
        }
        public string Name
        {
            get { return name; }
        }
        public NGEBone Parent
        {
            get { return parent; }
        }
        public NGEChannel[] Channels
        {
            get { return channels; }
        }
  
        public NGEBone(NGEBone Parent, string Name, int nrChannels, TransAxis Axis, bool IsKinectJoint)
        {
            parent = Parent;
            index += index;
            name = Name;
            isKinectJoint = IsKinectJoint;
            axis = Axis;
            if (parent != null)
                depth = parent.Depth + 1;
            else
            {
                depth = 0;
                root = true;
            }
            channels = new NGEChannel[nrChannels];
            int ind = 5;
            for (int k = nrChannels-1; k >= 0; k--)
            {
                channels[k] = (NGEChannel)ind;
                ind--;
            }
        }

        public void setTransOffset(double xOff,double yOff,double zOff)
        {
            translOffset = new double[]{xOff, yOff, zOff};
        }

        public void setRotOffset(double xOff, double yOff, double zOff)
        {
            rotOffset = new double[] { xOff, yOff, zOff };
        }
    }
}
