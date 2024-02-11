using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject cameraMover;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioClip[] winMusics;
    [SerializeField] private Animator[] animators;
    [SerializeField] private GameObject[] guns;
    [SerializeField] private GameObject backMusic;
    [SerializeField] private AudioClip[] playerWords;
    [SerializeField] private GameObject[] sigmas;
    [SerializeField] private GameObject mainSigma;

    [SerializeField] private Vector3 winCameraRotation;
    [SerializeField] private Vector3 winCameraPosition;
    [SerializeField] private Vector3 maxSpeed;
    [SerializeField] private float speed = 20f;

    [SerializeField] private float leftBorder;
    [SerializeField] private float rightBorder;

    [SerializeField] private LayerMask controlMask;

    private int lastM = -1;
    private Rigidbody rb;
    private AudioSource source;
    private Vector3 lastPos, lastMouse;

    // private float lastInputX;
    // private bool wasMoving;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        lastPos = transform.position;
    }

    void Update()
    {
        if (!source.isPlaying)
        {
            if (Game.Instance.Win)
            {
                int m = Random.Range(0, winMusics.Length);
                while (m == lastM)
                {
                    m = Random.Range(0, winMusics.Length);
                }
                lastM = m;
                source.volume = 1;
                source.PlayOneShot(winMusics[m]);
            }
            else
            {
                source.PlayOneShot(audioClips.GetRandom());
            }
        }

        if (Input.GetMouseButton(0))
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, controlMask))
            {
                var position = hit.point.x;
                // if (!wasMoving) lastInputX = position;
                // var delta = position - lastInputX;
                // lastInputX = position;

                // var old = position;
                var newPosition = Util.ClampedMap(position, leftBorder, rightBorder, leftBorder, rightBorder);

                // Debug.Log($"hit: {position} calculated: {newPosition}");

                mainSigma.transform.SetX(Mathf.Lerp(mainSigma.transform.position.x, newPosition, maxSpeed.x * speed * Time.deltaTime));
            }
            // wasMoving = true;
        }
        // else
        // wasMoving = false;

        cameraMover.transform.position += new Vector3(0, 0, transform.position.z - lastPos.z);

        if (Game.Instance.EnemyCount <= 0 && !Game.Instance.Win)
        {
            Win();
            Game.Instance.Win = true;
        }

        lastPos = transform.position;
        lastMouse = Input.mousePosition;
    }

    private void FixedUpdate()
    {
        rb.velocity = speed * Time.deltaTime * new Vector3(0, 0, maxSpeed.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemies>(out _))
            Death();

        if (other.TryGetComponent<TakeGun>(out _))
        {
            foreach (var gun in guns)
                gun.SetActive(true);

            backMusic.GetComponent<AudioSource>().Play();
        }

        if (other.TryGetComponent<Hostage>(out _))
        {
            foreach (GameObject obj in sigmas)
                obj.SetActive(true);

            backMusic.GetComponent<AudioSource>().Play();
        }
    }

    private void Death()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("isDied");
            animator.SetBool("isRun", false);
        }
        speed = 0;
        foreach (var gun in guns)
            gun.SetActive(false);

        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(5);
        Game.Instance.Restart();
    }

    [ContextMenu("Test/Win")]
    public void Win()
    {
        mainSigma.transform.position = new Vector3(0, 0, mainSigma.transform.position.z);

        source.Stop();

        int m = Random.Range(0, winMusics.Length);
        lastM = m;
        source.PlayOneShot(winMusics[m]);

        var backAudio = backMusic.GetComponent<AudioSource>();

        backAudio.Stop();
        backAudio.PlayOneShot(playerWords.GetRandom());

        cam.transform.position = mainSigma.transform.position + winCameraPosition;
        cam.transform.localEulerAngles = winCameraRotation;

        int animA = Random.Range(0, 3);
        int animB = Random.Range(0, 3);
        while (animB == animA)
        {
            animB = Random.Range(0, 3);
        }

        animators[0].SetInteger("DanceAnim", animA);
        for (int i = 1; i < animators.Length; i++)
        {
            animators[i].SetInteger("DanceAnim", animB);
        }

        speed = 0;
        foreach (var gun in guns)
            gun.SetActive(false);
    }
}
