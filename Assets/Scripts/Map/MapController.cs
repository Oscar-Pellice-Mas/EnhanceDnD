using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static MapController Instance { get; private set; }

    private const int size = 20;

    private Dictionary<string, Tile> tileMap = new Dictionary<string,Tile>();

    [SerializeField] private GameObject collisionPrefab;

    public class Tile
    {
        public int x, y, z;
        

        public Tile(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override string ToString()
        {
            return "Tile[" + x + "," + y + "," + z + "]";
        }
    }

    private void Awake()
    {
        // Instance creation
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        // Map init
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Tile tile = new Tile(i,0,j);
                tileMap.Add(tile.ToString(), tile);
                GameObject colision = GameObject.Instantiate(collisionPrefab, new Vector3(i, 0.05f, j), Quaternion.identity, this.transform);
                colision.name = tile.ToString();
            }
        }
    }

    public Tile GetTile(string name)
    {
        return tileMap[name];
    }
}
