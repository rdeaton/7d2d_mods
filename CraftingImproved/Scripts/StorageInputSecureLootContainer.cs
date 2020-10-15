using UnityEngine;

public class BlockStorageInputSecureLoot : BlockSecureLoot {

	public override void OnBlockAdded(WorldBase world, Chunk _chunk, Vector3i _blockPos, BlockValue _blockValue)
	{
		base.OnBlockAdded(world, _chunk, _blockPos, _blockValue);
		if (_blockValue.ischild)
		{
			return;
		}
		if (!(world.GetTileEntity(_chunk.ClrIdx, _blockPos) is ImprovedTileEntitySecureLootContainer))
		{
			ImprovedTileEntitySecureLootContainer tileEntitySecureLootContainer = new ImprovedTileEntitySecureLootContainer(_chunk);
			tileEntitySecureLootContainer.localChunkPos = World.toBlock(_blockPos);
			tileEntitySecureLootContainer.lootListIndex = (int)((ushort)this.lootList);
			tileEntitySecureLootContainer.SetContainerSize(LootContainer.lootList[this.lootList].size, true);
			_chunk.AddTileEntity(tileEntitySecureLootContainer);
		}
    }
}


public class ImprovedTileEntitySecureLootContainer : TileEntitySecureLootContainer {

    public ImprovedTileEntitySecureLootContainer(Chunk _chunk) : base(_chunk)
	{
    }

	public override void UpdateTick(World world)
	{
		base.UpdateTick(world);
        if (this.bUserAccessing) {
            // Do nothing for now to make sure we don't have race conditions.
            // We can maybe allow functionality to fall through later.
            return;
        }
        if (!this.IsEmpty()) {
            ItemStack clone_item = null;
            for (int i = 0; i < this.items.Length; i++) {
                if (!this.items[i].IsEmpty()) {
                    clone_item = this.items[i].Clone();
                }
            }
            this.TryStackItem(0, clone_item);
        }
        Debug.Log("AHH TICK");
    }

    public override TileEntityType GetTileEntityType()
	{
        // TileEntityType.SecureLootSigned is the highest member of the enum at 0x16.
		return (TileEntityType) 0x17;
	}
}