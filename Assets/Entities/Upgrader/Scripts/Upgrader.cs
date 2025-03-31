using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;
using drillex.Assets.Entities.Furnace;
using System.Reflection.Metadata.Ecma335;

namespace drillex.Assets.Entities.Upgrader;


public partial class Upgrader : TileMapLayer
{
	[Export] private Wallet _wallet;
	
	Node2D _materialHolder;

	//Location of upgraders
	//HashSet<Vector2> _upgraders;
	Godot.Collections.Dictionary _upgraders;


	public override void _Ready()
	{
		_materialHolder = GetNode<Node2D>("../MaterialHolder");
		_upgraders = new Godot.Collections.Dictionary();
	}

	public override void _PhysicsProcess(double delta)
	{
		foreach (var node in _materialHolder.GetChildren())
		{
			Material material = (Material)node;
			
			if (_upgraders.ContainsKey(material.Position.Snapped(32f))) 
			{
				UpgraderCell upgrader = (UpgraderCell)_upgraders[material.Position.Snapped(32f)];
				if (!material.Upgraded(upgrader))
				{
					material.UpgradeTier(upgrader);
				}
			}
		}
	}
	
	
	public bool Exists(Vector2I mapPosition)
	{
		if (GetCellTileData(mapPosition) != null)
		{
			if(GetCellTileData(mapPosition).GetCustomData("Type").AsString() == "Upgrader") return true;
		}
		return false;
	}


	public void AddUpgrader(Vector2I mapPosition)
	{
		SetCell(mapPosition, 0, new Vector2I(0, 0));
		UpgraderCell cell = new UpgraderCell();
		_upgraders.Add( (MapToLocal(mapPosition) - new Vector2I(16, 16)), cell);
	}

	public void RemoveUpgrader(Vector2I mapPosition)
	{
		EraseCell(mapPosition);
		_upgraders.Remove(MapToLocal(mapPosition) - new Vector2I(16, 16));
	}
}
