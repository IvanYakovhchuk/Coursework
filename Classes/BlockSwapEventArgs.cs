namespace Classes
{
    public class BlockSwapEventArgs(int index1, int index2) : EventArgs
    {
        public int Index1 { get; } = index1;
        public int Index2 { get; } = index2;
    }
}
