namespace Classes
{
    //class of event arguments
    public class BlockSwapEventArgs(int index1, int index2) : EventArgs
    {
        //contains the indexes of swapped elements to return them from the sorting algorhytm to the blocks panel
        public int Index1 { get; } = index1;
        public int Index2 { get; } = index2;
    }
}
