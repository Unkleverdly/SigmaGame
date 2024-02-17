using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternSpawner : MonoBehaviour
{
    [System.Serializable]
    public class PatternGroup
    {
        [SerializeField] private string name;
        [SerializeField] private Pattern[] patterns;
        public Pattern[] Patterns => patterns;
    }

    [SerializeField] private PatternGroup[] patterns;
    [SerializeField] private GameObject patternsHolder;

    [SerializeField] private Pattern start;

    [SerializeField] private int patternCountMin;
    [SerializeField] private int patternCountMax;

    [Range(0, 1), SerializeField] private float generateSigmaCoins;

    private List<Pattern> toEnable;

    private void Awake()
    {
        toEnable = new();

        var count = Random.Range(patternCountMin, patternCountMax + 1);

        var old = start;

        var groups = patterns.Length;

        for (int i = 0; i < count; i++)
        {
            var group = patterns[0];
            if (i == count - 1)
                group = patterns[^1];
            else if (i != 0)
                group = patterns[(int)((float)i / count * groups)];

            var toSpawn = group.Patterns.GetRandom();
            var pattern = Spawn(toSpawn, old.EndPoint);

            var genSigmaCoins = Random.Range(0, 1) <= generateSigmaCoins;

            pattern.Setup(i, genSigmaCoins);

            if (i != 0)
                toEnable.Add(pattern);

            old = pattern;
        }

    }

    private Pattern Spawn(Pattern pattern, Vector3 at)
    {
        var p = Instantiate(pattern);
        p.transform.SetParent(patternsHolder.transform);
        p.transform.position = at;
        return p;
    }
}
