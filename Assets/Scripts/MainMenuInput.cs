using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Button
{
    public Sprite Up {  get; private set; }
    public Sprite Down { get; private set; }

    public Button(Sprite up, Sprite down)
    {
        Up = up;
        Down = down;
    }
}

public class MainMenuInput : MonoBehaviour {
    private bool[] _prevInputs = new bool[6];
    private bool[] _curInputs = new bool[6];

    private Dictionary<KeyCode, int> _keyCodeIndexRef;
    private int _curOption = 0;

    private GameObject _start, _instructions, _exit;
    private Button _startBtn, _instrBtn, _exitBtn;

	// Use this for initialization
	void Start () {
        _keyCodeIndexRef = new Dictionary<KeyCode, int>();
        _keyCodeIndexRef.Add(KeyCode.LeftArrow, 0);
        _keyCodeIndexRef.Add(KeyCode.RightArrow, 1);
        _keyCodeIndexRef.Add(KeyCode.UpArrow, 2);
        _keyCodeIndexRef.Add(KeyCode.DownArrow, 3);
        _keyCodeIndexRef.Add(KeyCode.Z, 4);
        _keyCodeIndexRef.Add(KeyCode.X, 5);
        _start = GameObject.Find("StartBtn");
        _instructions = GameObject.Find("InstructBtn");
        _exit = GameObject.Find("ExitBtn");

        _startBtn = new Button(Resources.Load<Sprite>("StartBtn"), Resources.Load<Sprite>("StartBtnDown"));
        _instrBtn = new Button(Resources.Load<Sprite>("InstructBtn"), Resources.Load<Sprite>("InstructBtnDown"));
        _exitBtn = new Button(Resources.Load<Sprite>("ExitBtn"), Resources.Load<Sprite>("ExitBtnDown"));

        _start.GetComponent<SpriteRenderer>().sprite = _startBtn.Down;
    }

    void getInputs()
    {
        _curInputs[0] = Input.GetKeyDown(KeyCode.LeftArrow);
        _curInputs[1] = Input.GetKeyDown(KeyCode.RightArrow);
        _curInputs[2] = Input.GetKeyDown(KeyCode.UpArrow);
        _curInputs[3] = Input.GetKeyDown(KeyCode.DownArrow);
        _curInputs[4] = Input.GetKeyDown(KeyCode.Z);
        _curInputs[5] = Input.GetKeyDown(KeyCode.X);
    }

    void resetInput()
    {
        _curInputs.CopyTo(_prevInputs, 0);
    }

    bool isPressed(KeyCode key)
    {
        if (!_keyCodeIndexRef.ContainsKey(key))
        {
            return false;
        }

        int index = _keyCodeIndexRef[key];

        return !_prevInputs[index] && _curInputs[index];
    }
	
	// Update is called once per frame
	void Update () {
        getInputs();

        if(isPressed(KeyCode.LeftArrow))
        {

        }

        if(isPressed(KeyCode.RightArrow))
        {

        }

        if(isPressed(KeyCode.UpArrow))
        {
            if (--_curOption < 0)
            {
                _curOption = 2;
            }

            switch(_curOption)
            {
                case 0:
                    _start.GetComponent<SpriteRenderer>().sprite = _startBtn.Down;
                    _instructions.GetComponent<SpriteRenderer>().sprite = _instrBtn.Up;
                    break;
                
                case 1:
                    _instructions.GetComponent<SpriteRenderer>().sprite = _instrBtn.Down;
                    _exit.GetComponent<SpriteRenderer>().sprite = _exitBtn.Up;
                    break;

                case 2:
                    _start.GetComponent<SpriteRenderer>().sprite = _startBtn.Up;
                    _exit.GetComponent<SpriteRenderer>().sprite = _exitBtn.Down;
                    break;
            }
        }

        if(isPressed(KeyCode.DownArrow))
        {
            if(++_curOption > 2)
            {
                _curOption = 0;
            }

            switch (_curOption)
            {
                case 0:
                    _start.GetComponent<SpriteRenderer>().sprite = _startBtn.Down;
                    _exit.GetComponent<SpriteRenderer>().sprite = _exitBtn.Up;
                    break;

                case 1:
                    _start.GetComponent<SpriteRenderer>().sprite = _startBtn.Up;
                    _instructions.GetComponent<SpriteRenderer>().sprite = _instrBtn.Down;
                    break;

                case 2:
                    _instructions.GetComponent<SpriteRenderer>().sprite = _instrBtn.Up;
                    _exit.GetComponent<SpriteRenderer>().sprite = _exitBtn.Down;
                    break;
            }
        }

        if(isPressed(KeyCode.Z))
        {

        }

        if(isPressed(KeyCode.X))
        {

        }

        resetInput();
	}
}
