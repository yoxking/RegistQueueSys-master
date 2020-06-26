namespace EntFrm.Framework.Utility
{
    public class ItemObject
    {
        private string _Name;
        private object _Value;

        public ItemObject() { }
        public ItemObject(string name, object value)
        {
            _Name = name;
            _Value = value;
        }

        /// <summary>
        /// 对象引用
        /// </summary>
        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        /// <summary>
        /// 键
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// 返回该对象的说明
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
