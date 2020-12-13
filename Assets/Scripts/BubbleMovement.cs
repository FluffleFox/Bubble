using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public Vector3 speedDifrencess;

    Vector3 currentVelocity;
    Vector3 windDirection;

    bool dissolve = false;
    Material myMaterial;
    float currentDissolveValue = 0.0f;

    private void Awake()
    {
        myMaterial = GetComponent<Renderer>().material;
    }

    private void OnEnable()
    {
        dissolve = false;
        GetComponent<CircleCollider2D>().enabled = true;
        myMaterial.SetFloat("_Dissolve", 0.0f);
        currentDissolveValue = 0.0f;
        transform.localScale = Vector3.one;
        currentVelocity = Vector3.up* Random.Range(0.8f, 2.2f);
        windDirection = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.15f));
    }
    void Update()
    {
        currentVelocity += windDirection*Time.deltaTime;
        currentVelocity.y -= Time.deltaTime*0.2f;
        transform.Translate(currentVelocity * Time.deltaTime, Space.World);

        if (dissolve)
        {
            currentDissolveValue += Time.deltaTime*5.0f;
            transform.localScale = Vector3.one * (1.0f + currentDissolveValue);
            myMaterial.SetFloat("_Dissolve", currentDissolveValue);
            if (currentDissolveValue >= 1.0f)
            {
                transform.position = Vector3.down * 20.0f;
            }
        }

        if(Mathf.Abs(transform.position.x) > 0.57735f * transform.position.z+0.5f)
        {
            windDirection.x *= -1.0f;
            currentVelocity.x = currentVelocity.x * -0.5f;
            currentVelocity.y = Mathf.Abs(currentVelocity.y);
        }

        //Out of bounds 
        if (transform.position.y < -0.57735f * transform.position.z * Spawner.instance.screenRatio-0.5f || transform.position.z<-0.1f)
        {
            Spawner.instance.AddToList(this.gameObject);
            this.enabled = false;
        }
    }

    private void OnMouseDown()
    {
        if (GameStateMenager.instance.currentState == GameStateMenager.State.Play)
        {
            RotateTowardMouse();
            SoundMenager.instance.PlaySound(transform.position);
            GetComponent<CircleCollider2D>().enabled = false;
            dissolve = true;
            PlayerRecords.instance.AddScore();
            PlayerRecords.instance.SetFarestOrClosestPoint(transform.position.z);
        }
    }

    private void OnMouseEnter()
    {
        if (SlideCanon.instance.active)
        {
            OnMouseDown();
        }
    }

    void RotateTowardMouse()
    {
        Vector3 rayDir = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
        Vector3 hitPoint = rayDir * transform.position.z / rayDir.z;
        SlideCanon.instance.UpdateSlideData(rayDir);
        Vector3 dir = transform.position - hitPoint;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg);
    }
}
