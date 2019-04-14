using UnityEngine;
using System.Collections;
using Lean.Touch;

public class LeanTouchRotate : MonoBehaviour
{

    //	摄像机移动方向
    public enum MoveDirection
    {
        None = 0,       //	空 0
        Up = 1 << 0,    //	上 1
        Down = 1 << 1,  //	下 2
        Left = 1 << 2,  //	左 4
        Right = 1 << 3, //	右 8
    }
    Vector3 previousPosition;
    Vector3 offset;

    public float maxDistance;   //	摄像机最大距离
    public float minDistance;   //	摄像机最小距离
    public float moveSpeed; //	摄像机移动速度
    public float rotateSpeed;   //	摄像机旋转速度

    private int w = 0;  //	屏幕的宽
    private int h = 0;  //	屏幕的高
    private bool didClickRightButton = false;   //	鼠标右键是否按下
    private MoveDirection moveDirection = MoveDirection.None;   //	移动方向
    private Vector3 direction = Vector3.zero;   //	摄像机移动方向向量
    private Vector3 lastMousePos = Vector3.zero;	//	上一帧鼠标的位置

    [Tooltip("The camera the movement will be done relative to (None = MainCamera)")]
    public Camera Camera;

    [Tooltip("Ignore fingers with StartedOverGui?")]
    public bool IgnoreStartedOverGui = true;

    [Tooltip("Ignore fingers with IsOverGui?")]
    public bool IgnoreIsOverGui;

    [Tooltip("Ignore fingers if the finger count doesn't match? (0 = any)")]
    public int RequiredFingerCount;

    [Tooltip("The sensitivity of the movement, use -1 to invert")]
    public float Sensitivity = 1.0f;

    public GameObject center;


    float smooth = 5.0f;
    float tiltAngle = 60.0f;

    void Start()
    {
        w = Screen.width;
        h = Screen.height;
    }

    void Update()
    {

       // MoveCamera();
        if (LeanTouch.Fingers.Count == 1)
        {
            UpdateDirection();
            RotateCamera();
        }
    }




    //	移动摄像机
    private void RotateCamera()
    {
       // Debug.Log(transform.eulerAngles);
            //	判断摄像机是否应当移动
            if (moveDirection != MoveDirection.None)
            {
                //	重置方向向量
                direction = Vector3.zero;
                //	判断是否包含上下移动的方向
                if ((moveDirection & MoveDirection.Up) != 0)
                {
                    //	获取摄像机正前方
                    Vector3 tempForward = transform.forward;
                    tempForward.y = 0f;
                    tempForward.Normalize();
                    direction += tempForward;
               // Debug.Log("direction.x");

                transform.eulerAngles += ((transform.eulerAngles.x + 30 * Time.deltaTime * rotateSpeed) < 1 || (transform.eulerAngles.x + 30 * Time.deltaTime * rotateSpeed) > 40) ? new Vector3(0, 0, 0) : new Vector3(30 * Time.deltaTime * rotateSpeed, 0, 0);
            }
                else if ((moveDirection & MoveDirection.Down) != 0)
                {
                    //	获取摄像机正前方
                    Vector3 tempForward = transform.forward;
                tempForward.y = 0f;
                    //	获取单位向量
                    tempForward.Normalize();
                    direction += -tempForward;
               // Debug.Log("down");
                transform.eulerAngles += ((transform.eulerAngles.x - 30 * Time.deltaTime * rotateSpeed) < 1 || (transform.eulerAngles.x - 30 * Time.deltaTime * rotateSpeed) > 40) ? new Vector3(0, 0, 0) : new Vector3(-30 * Time.deltaTime * rotateSpeed, 0, 0);
            }
                //	判断是否包含左右移动的方向
                if ((moveDirection & MoveDirection.Left) != 0)
                {
                    //	将方向向量与向上移动的向量叠加
                    direction += -transform.right;
             //   Debug.Log("left");
                //transform.eulerAngles += (false) ? new Vector3(0, 0, 0) : new Vector3(0, 30 * Time.deltaTime * rotateSpeed, 0);
                center.transform.eulerAngles += (false) ? new Vector3(0, 0, 0) : new Vector3(0, 30 * Time.deltaTime * rotateSpeed, 0);
            }
                else if ((moveDirection & MoveDirection.Right) != 0)
                {
                    //	将方向向量与向下移动的向量叠加
                    direction += transform.right;
                //  Debug.Log("right");
                //transform.eulerAngles += (transform.position.y > maxDistance || transform.position.y < minDistance) ? new Vector3(0, 0, 0) : new Vector3(0, -30 * Time.deltaTime * rotateSpeed, 0);
                center.transform.eulerAngles += (false) ? new Vector3(0, 0, 0) : new Vector3(0,- 30 * Time.deltaTime * rotateSpeed, 0);
            }
                //	单位化方向向量
             direction.Normalize();
        }
    }

    private void UpdateDirection()
    {
        //	将当前移动方向设为空
        moveDirection = MoveDirection.None;
        //	获取鼠标当前位置
        Vector3 mousePos = Input.mousePosition;
        //	检测摄像机上下移动
       
        // 通过LeanTouch插件，来判断目前触碰屏幕的手指数量
        //if (LeanTouch.Fingers.Count == 2)
        //{

            // LeanTouch可以将鼠标点击和屏幕触碰进行转换
            if (Input.GetMouseButtonDown(0))
            {

                previousPosition = Input.mousePosition;

            }

            if (Input.GetMouseButton(0))
            {

                offset = Input.mousePosition - previousPosition;
                previousPosition = Input.mousePosition;
                float xdis = Mathf.Abs(offset.x);
                float ydis = Mathf.Abs(offset.y);
                if (xdis > ydis)
                {
                    if (offset.x > 0)
                    {
                        moveDirection |= MoveDirection.Left;
                    }

                    if (offset.x < 0)
                    {
                        moveDirection |= MoveDirection.Right;
                    }
                }
                else
                {
                    if (offset.y > 0)
                    {
                        moveDirection |= MoveDirection.Down;
                    }

                    if (offset.y < 0)
                    {
                         moveDirection |= MoveDirection.Up;
                    }
                }
            }

        //}
    }

    //	移动摄像机
    private void MoveCamera()
    {
        if (LeanTouch.Fingers.Count == 1)
        {
            //	判断摄像机是否应当移动
            if (moveDirection != MoveDirection.None)
            {
                //	重置方向向量
                direction = Vector3.zero;
                //	判断是否包含上下移动的方向
                if ((moveDirection & MoveDirection.Up) != 0)
                {
                    //	获取摄像机正前方
                    Vector3 tempForward = transform.forward;
                    //	将正前方向量映射到 XZ 平面上
                    tempForward.y = 0f;
                    //	获取单位向量
                    tempForward.Normalize();
                    //	将方向向量与向前移动的向量叠加
                    direction += tempForward;
                    transform.position += tempForward;
                }
                else if ((moveDirection & MoveDirection.Down) != 0)
                {
                    //	获取摄像机正前方
                    Vector3 tempForward = transform.forward;
                    //	将正前方向量映射到 XZ 平面上
                    tempForward.y = 0f;
                    //	获取单位向量
                    tempForward.Normalize();
                    //	将方向向量与向后移动的向量叠加
                    direction += -tempForward;
                    transform.position += -tempForward;
                }
                //	判断是否包含左右移动的方向
                if ((moveDirection & MoveDirection.Left) != 0)
                {
                    //	将方向向量与向上移动的向量叠加
                    direction += -transform.right;
                    transform.position += -transform.right;
                }
                else if ((moveDirection & MoveDirection.Right) != 0)
                {
                    //	将方向向量与向下移动的向量叠加
                    direction += transform.right;
                    transform.position += transform.right;
                }
                //	单位化方向向量
                direction.Normalize();
                //	移动摄像机
                //transform.position += direction * Time.deltaTime * moveSpeed;
            }
        }
    }
}
