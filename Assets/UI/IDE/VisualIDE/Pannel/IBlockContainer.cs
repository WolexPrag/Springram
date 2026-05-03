public interface IBlockContainer
{
    public void Insert(int index, Block block);
    public void Remove(int index);
    public int GetIndex(Block block);
}