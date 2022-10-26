using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MissionControl.Common
{
    public class HeapMax
    {
        public double[] Array { get; private set; }
        public int Length { get; private set; }

        public HeapMax(int size)
        {
            this.Length = 0;
            this.Array = new double[size];
            BuildMaxHeap();
        }

        private void BuildMaxHeap()
        {
            for (int i = this.Length / 2; i > 0; i--)
            {
                MaxHeapify(i);
            }

            return;
        }

        public void MaxHeapify(int index)
        {
            var left = 2 * index;
            var right = 2 * index + 1;

            int max = index;
            if (left <= this.Length && this.Array[left - 1] > this.Array[index - 1])
            {
                max = left;
            }

            if (right <= this.Length && this.Array[right - 1] > this.Array[max - 1])
            {
                max = right;
            }

            if (max != index)
            {
                double temp = this.Array[max - 1];
                this.Array[max - 1] = this.Array[index - 1];
                this.Array[index - 1] = temp;
                MaxHeapify(max);
            }

            return;
        }

        /*public void HeapifyUp(int index)
        {
            int parent = index / 2;
            // We are at root of the tree. Hence no more Heapifying is required.  
            if (index <= 1)
            {
                return;
            }
            // If Current value is bigger than its parent, then we need to swap  
            if (Array[index] > Array[parent])
            {
                double tmp = Array[index];
                Array[index] = Array[parent];
                Array[parent] = tmp;
            }
            HeapifyUp(parent);
        }/**/

        public double RemoveMaximum()
        {
            double maximum = this.Array[0];

            this.Array[0] = this.Array[this.Length - 1];
            this.Length--;
            MaxHeapify(1);
            return maximum;
        }

        public void InsertElement(double value)
        {

            //Insertion of value inside the array happens at the last index of the  array
            Array[Length] = value;
            Length++;
            BuildMaxHeap();
            //MaxHeapify(1);
            //HeapifyUp(Length - 1);
        }

        public double PeekOfHeap()
        {
            if (Length == 0)
                return double.MaxValue;
            else
                return Array[0];
        }
    }
}