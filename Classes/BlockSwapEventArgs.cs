namespace Classes
{
    public class BlockSwapEventArgs : EventArgs
    {
        public int Index1 { get; }
        public int Index2 { get; }

        public BlockSwapEventArgs(int index1, int index2)
        {
            Index1 = index1;
            Index2 = index2;
        }
    }
}
