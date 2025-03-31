using Godot;
using System.Collections.Generic;

public partial class Material : Sprite2D
{
	[Export] public ulong MonetaryValue { get; set; }
	[Export] private Sprite2D material;
	public int currentTier = 0;
	private int maxTier = 5;
	List<UpgraderCell> _upgrades;
	
	public override void _Ready()
	{
		material = GetNode<Sprite2D>(".");
		Position = Position.Snapped(32f);
		_upgrades = new List<UpgraderCell>();
	}

	public override void _PhysicsProcess(double delta)
	{

	}

	public bool Upgraded(UpgraderCell upgrader)
	{
		if(_upgrades.Contains(upgrader)) { return true; }
		return false;
	}

	public void UpgradeTier(UpgraderCell upgrader)
	{
		if(currentTier < maxTier)
		{
			currentTier++;
			UpdateSprite();
		}
		_upgrades.Add(upgrader);
	}
	
	private void UpdateSprite(){
		Rect2 newRegion = material.RegionRect;
		newRegion.Position = new Vector2(currentTier * 32, 0);
		material.RegionRect = newRegion;
	}
}
