using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace NeonSpace
{
    public class RandomStarSystem : MonoBehaviour
    {

        private Texture RandomStarTexture
        {
            get
            {
                if (starTextures != null && starTextures.Length > 0)
                {
                    return starTextures[Random.Range(0, starTextures.Length)];
                }
                return null;
            }
        }
        private Texture RandomCoronaTexture
        {
            get
            {
                if (coronaTextures != null && coronaTextures.Length > 0)
                {
                    return coronaTextures[Random.Range(0, coronaTextures.Length)];
                }
                return null;
            }
        }

        // This property will return a random texture stored in: planetTextures
        private Texture RandomPlanetTexture
        {
            get
            {
                if (planetTextures != null && planetTextures.Length > 0)
                {
                    return planetTextures[Random.Range(0, planetTextures.Length)];
                }

                return null;
            }
        }

        // This property will return a random texture stored in: gasGiantTextures
        private Texture RandomGasGiantTexture
        {
            get
            {
                if (gasGiantTextures != null && gasGiantTextures.Length > 0)
                {
                    return gasGiantTextures[Random.Range(0, gasGiantTextures.Length)];
                }

                return null;
            }
        }

        // This property will return a random colour stored in the Color class.
        private Color RandomColour
        {
            get
            {
                switch (Random.Range(0, 6))
                {
                    case 0: return new Color(81, 207, 238, 255);
                    case 1: return new Color(173, 202, 180, 255);
                    case 2: return new Color(221, 193, 154, 255);
                    case 3: return new Color(223, 227, 197, 255);
                    case 4: return new Color(81, 207, 238, 255);
                    case 5: return new Color(229, 90, 132, 255);
                    //case 6: return Color.white;
                    case 6: return Color.yellow;
                }
                return Color.yellow;
            }
        }

        public Mesh surfaceMesh;
        public Mesh atmosphereMesh;

        public Texture[] starTextures;
        public Texture[] coronaTextures;
        public Texture[] planetTextures;
        public Texture[] gasGiantTextures;

        private SGT_Star _Star;

        [SerializeField]
        [HideInInspector]
        private List<GameObject> stuffInSystem = new List<GameObject>();

        private void Awake()
        {
            EventManager.Subscribe<GameStateMessage>(GameStateHandler);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            /*Vector3 tmpPos = Camera.main.WorldToScreenPoint(transform.position);
            if (tmpPos.x < -Screen.width)
            {
                Debug.Log("Clear star system");
                //Destroy(gameObject);
                Clear();
            }*/
        }

        private void GameStateHandler(GameStateMessage gameStateMessage)
        {
            if(gameStateMessage.GameState == GameState.Launch)
            {
                GenerateStarSystem();
                transform.localPosition = new Vector3(Random.Range(-10,30),0,50);
            }
            if(gameStateMessage.GameState == GameState.Menu)
            {
                Clear();
            }
        }

        private void AddStar()
        {
            var item = CreateItem("Procedural Star");
            GenerateStar(item);

            item.transform.localPosition = Vector3.zero;
        }
        private void AddPlanet()
        {
            var item = CreateItem("Procedural Planet");

            GenerateOrbit(item);
            GeneratePlanet(item);
        }
        private void AddGasGiant()
        {
            var item = CreateItem("Procedural Gas Giant");

            GenerateOrbit(item);

            GenerateGasGiant(item);
        }
        private void Clear()
        {
            foreach (var item in stuffInSystem)
            {
                Destroy(item.gameObject);
            }

            stuffInSystem.Clear();
        }


        private GameObject CreateItem(string newName)
        {
            var newItem = new GameObject(newName);

            newItem.transform.parent = transform;
            newItem.transform.rotation = Random.rotation;
            stuffInSystem.Add(newItem);

            return newItem;
        }

        private void GenerateOrbit(GameObject item)
        {
            var orbit = item.AddComponent<SGT_SimpleOrbit>();

            orbit.Orbit = true;

            orbit.OrbitAngle = Random.Range(-Mathf.PI, -Mathf.PI / 2);
            if (RandomBool() == true)
            {
                orbit.OrbitAngle = Random.Range(Mathf.PI / 2, Mathf.PI);
            }

            orbit.OrbitDistance = Random.Range(10.0f, 40.0f);

            orbit.Rotation = true;

            orbit.RotationPeriod = Random.Range(60.0f, 240.0f);
        }

        protected void GenerateStar(GameObject item)
        {
            SGT_Star star = item.AddComponent<SGT_Star>();
            item.AddComponent<SGT_LightSource>();

            star.SurfaceMesh.ReplaceAll(surfaceMesh, 0);
            star.AtmosphereMesh = atmosphereMesh;
            star.SurfaceTexture.SetTexture(RandomStarTexture, 0);
            star.SurfaceRadius = Random.Range(0.2f, 1);

            /*var atmosphereColour = star.AtmosphereDensityColour;
            atmosphereColour.AddColourNode(RandomColour, 0.5f);
            atmosphereColour.AddAlphaNode(1.0f, Random.Range(0.0f, 0.45f));
            atmosphereColour.AddAlphaNode(0.5f, Random.Range(0.5f, 1.0f));*/

            if (RandomBool() == true)
            {
                SGT_Corona corona = item.AddComponent<SGT_Corona>();
                corona.MeshType = SGT_Corona.Type.Ring;
                corona.CoronaTexture = RandomCoronaTexture;
                corona.MeshRadius = star.SurfaceRadius;
                corona.CoronaOffset = 0;
                corona.MeshHeight = Random.Range(corona.MeshRadius, corona.MeshRadius * 10);
                //corona.MeshHeight = corona.MeshRadius * 10;
                var coronaColour = corona.CoronaColour;
                //coronaColour = atmosphereColour.ColourNodes[0].Colour;
                coronaColour.a = 0.5f;
            }

            _Star = star;
        }

        protected void GeneratePlanet(GameObject item)
        {
            var planet = item.AddComponent<SGT_Planet>();

            planet.SurfaceMesh.ReplaceAll(surfaceMesh, 0);

            planet.SurfaceTextureDay.SetTexture(RandomPlanetTexture, 0);

            planet.SurfaceRadius = Random.Range(0.4f, 3.0f);

            var lighting = planet.PlanetLighting;
            lighting.AddColourNode(new Color(0,0,0,0.5f), Random.Range(0.0f, 0.45f));
            lighting.AddColourNode(new Color(1,1,1,0.5f), Random.Range(0.55f, 1.0f));

            var twilight = planet.AtmosphereTwilightColour;
            twilight.AddColourNode(RandomColour, 0.5f);
            twilight.AddAlphaNode(1.0f, Random.Range(0.0f, 0.45f));
            twilight.AddAlphaNode(0.0f, Random.Range(0.5f, 1.0f));

            if (RandomBool() == true)
            {
                planet.Atmosphere = true;
                planet.AtmosphereMesh = atmosphereMesh;
                planet.AtmosphereHeight = Random.Range(0.2f, 1f);
                planet.AtmosphereFog = Random.Range(0.0f, 0.25f);
                planet.AtmosphereFalloffInside = Random.Range(0.01f, 0.25f);
            }
        }

        protected void GenerateGasGiant(GameObject item)
        {
            // Add gas giant component
            var gasGiant = item.AddComponent<SGT_GasGiant>();

            // Randomise gas giant parameters
            gasGiant.GasGiantMesh = surfaceMesh;

            gasGiant.AtmosphereTextureDay = RandomGasGiantTexture;
            gasGiant.AtmosphereRadius = Random.Range(1.0f, 4.0f);
            gasGiant.AtmosphereDensity = Random.Range(3.0f, 10.0f);
            gasGiant.AtmosphereDensityFalloff = Random.Range(1.0f, 10.0f);
            gasGiant.AtmosphereOblateness = Random.Range(0.0f, 0.2f);

            var lighting = gasGiant.AtmosphereLighting;
            lighting.AddColourNode(new Color(1,1,1,0.5f), 0.5f);
            lighting.AddAlphaNode(1.0f, Random.Range(0.0f, 0.45f));
            lighting.AddAlphaNode(0.0f, Random.Range(0.5f, 1.0f));

            var twilight = gasGiant.AtmosphereTwilightColour;
            twilight.AddColourNode(RandomColour, 0.5f);
            twilight.AddAlphaNode(1.0f, Random.Range(0.0f, 0.45f));
            twilight.AddAlphaNode(0.0f, Random.Range(0.5f, 1.0f));

            var limb = gasGiant.AtmosphereLimbColour;
            limb.AddColourNode(RandomColour, 0.5f);
            limb.AddAlphaNode(1.0f, Random.Range(0.0f, 0.45f));
            limb.AddAlphaNode(0.0f, Random.Range(0.5f, 1.0f));
        }

        public void GenerateStarSystem()
        {
            Clear();
            transform.localPosition = new Vector3(transform.localPosition.x, Random.Range(-4, 4), transform.localPosition.z);
            int planetsAmount = Random.Range(2, 5);

            AddStar();

            for (int i = 0; i < planetsAmount; i++)
            {
                if (RandomBool() == true)
                {
                    AddPlanet();
                }
                else
                {
                    AddGasGiant();
                }
            }
        }

        // This function will return true 50% of the time
        protected bool RandomBool()
        {
            return Random.value > 0.5f;
        }
    }
}