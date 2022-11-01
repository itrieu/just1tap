using System;
using UnityEngine;

public class LevelLoader
{
	public LevelLoader ()
	{
	}

	public void MakeLevel(GameWorld world, GameObject floor, string levelstring)
	{
		
		FloorObject component = createGameEntity<FloorObject>(floor, world.transform);
		world.addObject(component);
		component.flashycolor = true;

        int col = 0;
        int row = 0;
        int seqId = 1;
		for (int i = 0; i < levelstring.Length; i++)
		{
			char c = levelstring[i];
			switch (c)
			{
			case 'R':
                    col++;
                    break;
            case 'U':
                    row++;
                break;
            default:
                    if (c == 'D')
    			    {
    				    row--;
                        break;
    			    }
    			    if (c == 'L')
    			    {
                        col--;
    			    }
                    break;
			}

			FloorObject component2 = createGameEntity<FloorObject>(floor, world.transform);
			component2.transform.localPosition = new Vector3((float)col, (float)row, 0f);
			world.addObject(component2);

			component2.direction = levelstring[i];
			component2.seqID = seqId;

			bool flag = true;
			while (flag && i < levelstring.Length - 1)
			{
				flag = false;
				if (levelstring[i + 1] != 'L' && levelstring[i + 1] != 'U' && levelstring[i + 1] != 'R' && levelstring[i + 1] != 'D')
				{
					i++;
					flag = true;
				}
				if (levelstring[i] == 'H')
				{
					i++;
					int colourschemechange = (int)(levelstring[i] - '0');
					component2.colourschemechange = colourschemechange;
				}
				if (i > 1 && levelstring[i - 1] != 'H')
				{
					int num4 = (int)(levelstring[i] - '0');
					if (num4 >= 0 && num4 <= 8)
					{
//						component2.SetToPortal(this.WorldToLevelNum(num4), false);
					}
					if (levelstring[i] == '9')
					{
						component2.SetToPortal(-2, false);
					}
				}
				if (levelstring[i] == '*')
				{
					component2.SetToPortal(-3, false);
				}
				if (levelstring[i] == 'S')
				{
					component2.Speed = 0.25f;
				}
				else if (levelstring[i] == 'X')
				{
					component2.Speed = 0.5f;
				}
				else if (levelstring[i] == '>')
				{
					component2.iconsprite.sprite = component2.sprIconFast;
					component2.IsSpecial = true;
					component2.sfxnum = 2;
				}
				else if (levelstring[i] == '<')
				{
					component2.iconsprite.sprite = component2.sprIconSlow;
					component2.IsSpecial = true;
					component2.sfxnum = 1;
				}
				else if (levelstring[i] == 'O')
				{
					component2.rotatecamera = 90f;
					component2.iconsprite.sprite = component2.sprIconCamera;
				}
				else if (levelstring[i] == 'P')
				{
					component2.rotatecamera = 180f;
					component2.iconsprite.sprite = component2.sprIconCamera;
				}
			}
			if (i == levelstring.Length - 1)
			{
				component2.SetToPortal(-1, false);
			}
			seqId++;
		}

		for (int j = 0, n = world.Count-1; j < n; j++)
		{
			(world.getObject(j) as FloorObject).SetSpriteFromChar((world.getObject(j + 1) as FloorObject).direction);
		}

		int index = world.Count - 1;
		(world.getObject(index) as FloorObject).SetSpriteFromChar('E');
	}

	public T createGameEntity<T>(GameObject prefab, Transform root = null) where T: GameEntity
	{
		GameObject obj = UnityEngine.Object.Instantiate(prefab, Vector3.zero, default(Quaternion)) as GameObject;
		obj.transform.SetParent(root);
		T component = obj.GetComponent<T>();
		return component;
	}

}

