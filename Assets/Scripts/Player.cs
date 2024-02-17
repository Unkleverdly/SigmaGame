using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public AudioSource SoundSource => soundSource;

    [SerializeField] private Camera cam;
    [SerializeField] private GameObject cameraMover;
    [SerializeField] private Animator[] animators;
    [SerializeField] private GameObject[] guns;
    [SerializeField] private GameObject[] sigmas;
    [SerializeField] private GameObject mainSigma;
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private Vector3 winCameraRotation;
    [SerializeField] private Vector3 winCameraPosition;
    [SerializeField] private Vector3 maxSpeed;
    [SerializeField] private float speed = 20f;
    [SerializeField] private float cameraSpeed = 20f;

    [SerializeField] private float leftBorder;
    [SerializeField] private float rightBorder;

    [SerializeField] private LayerMask controlMask;

    private int lastM = -1;
    private Rigidbody rb;
    private float lastTapX, lastSigmaPos;
    private bool won;

    private void Awake()
    {
        Instance = this;

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!musicSource.isPlaying)
        {
            if (Game.Instance.Win)
            {
                var winMusic = ContentManager.Instance.WinMusic;
                int m = Random.Range(0, winMusic.Count);
                while (m == lastM)
                {
                    m = Random.Range(0, winMusic.Count);
                }
                lastM = m;
                musicSource.volume = 1;
                musicSource.PlayOneShot(winMusic[m]);
            }
            else
            {
                musicSource.PlayOneShot(ContentManager.Instance.BackgroundMusic.GetRandom());
            }
        }

        if (!Game.Instance.Win)
        {
            var justPressed = Input.GetMouseButtonDown(0);

            if (Input.GetMouseButton(0))
            {
                var ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, Mathf.Infinity, controlMask))
                {
                    var position = hit.point.x;
                    if (justPressed)
                    {
                        lastTapX = position;
                        lastSigmaPos = mainSigma.transform.position.x;
                    }

                    var relative = position - lastTapX;


                    if (relative < leftBorder || relative > rightBorder)
                    {
                        lastTapX = position;
                        lastSigmaPos = mainSigma.transform.position.x;
                    }

                    var newPosition = Util.ClampedMap(position - lastTapX + lastSigmaPos, leftBorder, rightBorder, leftBorder, rightBorder);

                    mainSigma.transform.SetX(Mathf.Lerp(mainSigma.transform.position.x, newPosition, maxSpeed.x * speed * Time.deltaTime));
                }
            }

            var sigmaPos = mainSigma.transform.position.WithX(0);

            cameraMover.transform.position = Vector3.Lerp(cameraMover.transform.position, sigmaPos, Time.deltaTime * cameraSpeed);
        }

        if (!won && Game.Instance.Win)
            Win();
    }

    private void FixedUpdate()
    {
        rb.velocity = speed * Time.deltaTime * new Vector3(0, 0, maxSpeed.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out _))
            Death();

        if (other.TryGetComponent<TakeGun>(out _))
        {
            foreach (var gun in guns)
                gun.SetActive(true);
        }

        if (other.TryGetComponent<Hostage>(out _))
        {
            foreach (GameObject obj in sigmas)
                obj.SetActive(true);
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
        won = true;
        mainSigma.transform.SetX(0);
        cameraMover.transform.SetX(0);

        musicSource.Stop();

        var winMusic = ContentManager.Instance.WinMusic;
        int m = Random.Range(0, winMusic.Count);
        lastM = m;
        musicSource.PlayOneShot(winMusic[m]);

        SoundSource.PlayOneShot(ContentManager.Instance.WinSounds.GetRandom());

        cam.transform.position = Vector3.forward * mainSigma.transform.position.z + winCameraPosition;
        cam.transform.localEulerAngles = winCameraRotation;

        cam.fieldOfView = 95;

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
