namespace EntFrm.Framework.Utility
{
    public interface InterFilter
    {
        /**
        * 执行过滤信息
        * @param source 原始信息
        * @return 过滤器本身
        */
        InterFilter filter(string[] source);

    }
}
