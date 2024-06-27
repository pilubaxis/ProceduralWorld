# Procedural World

This project demonstrates a procedural world generation system in Unity using C#. The world is divided into a 3x3 grid of chunks, with the player always positioned in the middle chunk. As the player moves, the three chunks behind the player's movement direction are repositioned in front of the player. Each chunk is populated with objects defined by scriptable objects.

![Procedura World](https://github.com/pilubaxis/ProceduralWorld/assets/98563755/6f795b09-f7ed-4329-b7b7-08e0a118f644)
![Chunk Movement](https://github.com/pilubaxis/ProceduralWorld/assets/98563755/33c6bf0f-3ce5-4dd4-a492-89a01bd8b057)

## Features

### Chunks and Grid
- The world consists of 9 chunks arranged in a 3x3 grid.
- The player always remains in the center chunk.
- Chunks are dynamically repositioned as the player moves.

### Object Population
- Each chunk is populated with objects configured via scriptable objects.
- Scriptable objects specify the details of each object in the chunks.
  
  ![Objects Info](https://github.com/pilubaxis/ProceduralWorld/assets/98563755/d6fd21ab-7c55-40b0-be9a-d611e1b1b7ab)


## Scripts

### ObjectInfo
- A ScriptableObject that contains information for each object within a chunk.

### ObjectsManager
- Manages the population of each chunk with objects based on the specifications from the scriptable objects.

### PlayerMovement
- Provides simple first-person player movement.

### ChunkManager
- Manages the information for each chunk.

### HeightGenerator
- Generates the height of each chunk using Perlin noise.

### TerrainGenerator
- Sets up the 3x3 matrix of terrains and updates them based on the player's location.

## Future Improvements

- **Different Biomes:** Implement a variety of biomes, including transition chunks between different biomes.
- **Height Generation Variety:** Enhance height generation to include more variations.
- **Chunk Information Saving:** Implement saving of each chunk's information for persistence.
- **Map Creator:** Develop a tool for creating maps based on the procedural generation system.

## Getting Started

1. Clone this repository.
2. Open the project in Unity 2022.3.17f1.
3. Attach the relevant scripts to the GameObjects in your scene.
4. Configure the ScriptableObjects as needed.
5. Play the scene and explore the procedurally generated world!

## Assets Used

- [Polygon Trees](https://assetstore.unity.com/packages/3d/vegetation/trees/polygon-trees-224068)
- [Customizable Skybox](https://assetstore.unity.com/packages/2d/textures-materials/sky/customizable-skybox-174576)

## Contributing

Feel free to fork this repository, make improvements, and submit pull requests. Any contributions are welcome!

## License

This project is licensed under the MIT License. See the LICENSE file for details.
