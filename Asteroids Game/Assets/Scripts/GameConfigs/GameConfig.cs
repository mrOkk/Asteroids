using Core;
using Input;
using Spawning.Factories;
using UnityEngine;
using UnityEngine.Serialization;

namespace Configs
{
	[CreateAssetMenu(fileName = "GameConfig", menuName = "Asteroids/GameConfig")]
	public class GameConfig : ScriptableObject
	{
		public PlayerConfig Player;

		public CoreLoopRunner CoreLoopRunner;
		public InputHandlingBehaviour InputHandler;

		public float EnemySpawnTime = 2f;
		[Range(0.1f, 3f)]
		public float SpawnOffsetFromEdge = 0.5f;

		public LevelFactory LevelFactory;
	}
}
