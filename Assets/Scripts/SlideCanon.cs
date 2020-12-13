using UnityEngine;

public class SlideCanon : MonoBehaviour
{
    public static SlideCanon instance;
    public bool active = false;
    // for slide
    Vector3 lastHitPoint = Vector3.zero;
    Vector3 previousPoint = Vector3.zero;
    uint seriesCount = 0;
    float druation;
    // for hit in row
    float second;
    uint destroy;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpdadeHitInRow();
            active = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            active = false;
            previousPoint = Vector3.zero;
            lastHitPoint = Vector3.zero;
            seriesCount = 0;
        }

        if (druation >= 0.0f)
        {
            druation -= Time.deltaTime;
        }
        else
        {
            previousPoint = Vector3.zero;
            lastHitPoint = Vector3.zero;
            PlayerRecords.instance.SetSilce(seriesCount);
            seriesCount = 0;
        }

        if (second >= 0.0f)
        {
            second -= Time.deltaTime;
            if (second <= 0.0f)
            {
                PlayerRecords.instance.SetDestructionSpeed(destroy);
                destroy = 0;
            }
        }
    }

    public void UpdateSlideData(Vector3 hitPoint)
    {
        if(previousPoint!=Vector3.zero && lastHitPoint != Vector3.zero)
        {
            Vector3 from = lastHitPoint - previousPoint;
            Vector3 to = hitPoint - previousPoint;
            float angle = Vector3.Angle(from, to);
            if(angle<=155.0f) //manewrowość
            {
                Debug.DrawLine(lastHitPoint, previousPoint, Color.green, 300);
                Debug.DrawLine(hitPoint, previousPoint, Color.red, 300);
                previousPoint = Vector3.zero;
                lastHitPoint = Vector3.zero;
                PlayerRecords.instance.SetSilce(seriesCount);
                seriesCount = 0;
            }
        }
        druation = 0.1f;
        seriesCount++;
        lastHitPoint = previousPoint;
        previousPoint = hitPoint;

        if (second <= 0.0f)
        {
            second = 1.0f;
            destroy = 1;
        }
        else { destroy++; }
    }

    void UpdadeHitInRow()
    {
        if (GameStateMenager.instance.currentState != GameStateMenager.State.Play) { return; }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        PlayerRecords.instance.SetHitInRow((uint)Physics2D.RaycastAll(ray.origin, ray.direction).Length);
    }
}