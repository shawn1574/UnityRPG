using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{

    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;
    float _pressedTime = 0;
   public void OnUpdate()//camera#2
    {
        if (EventSystem.current.IsPointerOverGameObject())//버튼 눌리면 캐릭터 움직이지 않게함
            return;

        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();

        if(MouseAction!=null)
        {
            if (Input.GetMouseButton(0))
            {
                if(!_pressed)
                {
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                {
                    if(Time.time<_pressedTime+0.2f)
                    {
                        MouseAction.Invoke(Define.MouseEvent.Click);
                    }
                   

                    MouseAction.Invoke(Define.MouseEvent.PointerUp);
                }
                   
                _pressed = false;
                _pressedTime = 0;
            }
        }
      
          
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
