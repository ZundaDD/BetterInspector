using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace MikanLab
{
    /// <summary>
    /// 枚举数组
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    [Serializable]
    public class EnumArray<TEnum, TValue> where TEnum : Enum
    {
        /// <summary>
        /// 实际数组
        /// </summary>
        [SerializeField] private TValue[] array;
        
        /// <summary>
        /// 对应的索引
        /// </summary>
        static private Dictionary<TEnum, int> CorInd;

        static EnumArray()
        {
            CorInd = new Dictionary<TEnum, int>();
            int ind = 0;
            foreach (TEnum EnumValue in Enum.GetValues(typeof(TEnum)))
            {
                CorInd[EnumValue] = ind;
                ind++;
            }
        }

        public EnumArray()
        {
            int size = Enum.GetValues(typeof(TEnum)).Length;
            array = new TValue[size];
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="_enum">枚举值</param>
        /// <returns>对应值</returns>
        public TValue this[TEnum _enum]
        {
            get => array[CorInd[_enum]]; 
            set => array[CorInd[_enum]] = value;
        }


    }

    /// <summary>
    /// 二维枚举数组
    /// </summary>
    /// <typeparam name="TEnum1">枚举一</typeparam>
    /// <typeparam name="TEnum2">枚举二</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    public class DEnumArray<TEnum1, TEnum2, TValue>
    where TEnum1 : Enum
    where TEnum2 : Enum
    {
        private TValue[] array;

        public DEnumArray()
        {
            int size1 = Enum.GetValues(typeof(TEnum1)).Length;
            int size2 = Enum.GetValues(typeof(TEnum2)).Length;
            array = new TValue[size1 * size2];
        }

        public TValue this[TEnum1 enum1, TEnum2 enum2]
        {
            get => array[CalculateIndex(enum1, enum2)]; 
            set => array[CalculateIndex(enum1, enum2)] = value;
        }

        /// <summary>
        /// 计算索引
        /// </summary>
        /// <param name="enum1">枚举值1</param>
        /// <param name="enum2">枚举值2</param>
        /// <returns>实际索引</returns>
        private int CalculateIndex(TEnum1 enum1, TEnum2 enum2)
        {
            int index1 = Convert.ToInt32(enum1);
            int index2 = Convert.ToInt32(enum2);
            int size2 = Enum.GetValues(typeof(TEnum2)).Length;
            return index1 * size2 + index2;
        }
    }
}