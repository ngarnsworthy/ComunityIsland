using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeapSort<T> where T : IComparable<T>
{
    class Item
    {
        public Item parent;
        public Item left;
        public Item right;
        public T value;

        public Item(Item parent)
        {
            this.parent = parent;
        }

        public List<T> Values
        {
            get
            {
                List<T> values = new List<T>();
                if(left != null)
                {
                    values.AddRange(left.Values);
                }
                values.Add(value);
                if (right != null)
                {
                    values.AddRange(right.Values);
                }
                return values;
            }
        }

        public void Add(T value)
        {
            if (value.CompareTo(value)>0)
            {
                if(left != null)
                {
                    left.Add(value);
                }
                else
                {
                    left = new Item(this);
                    left.value = value;
                }
            }
            else
            {
                if (right != null)
                {
                    right.Add(value);
                }
                else
                {
                    right = new Item(this);
                    right.value = value;
                }
            }
        }
    }
}
