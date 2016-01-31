using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Button
{
    public const int BUTTON_UP = 0;
    public const int BUTTON_DOWN = 1;

    public Sprite Up {  get; private set; }
    public Sprite Down { get; private set; }

    private SpriteRenderer _renderer;

    public Button(Sprite up, Sprite down, SpriteRenderer renderer)
    {
        Up = up;
        Down = down;
        _renderer = renderer;
    }

    public Button(Sprite up, Sprite down, GameObject obj) : this(up, down, obj.GetComponent<SpriteRenderer>()) { }

    public void changeState(int state)
    {
        if(state == BUTTON_UP)
        {
            _renderer.sprite = Up;
        }
        else if(state == BUTTON_DOWN)
        {
            _renderer.sprite = Down;
        }
    }
}

public class MainMenuInput : MonoBehaviour {
    private bool[] _prevInputs = new bool[6];
    private bool[] _curInputs = new bool[6];

    private Dictionary<KeyCode, int> _keyCodeIndexRef;
    private int _curOption = 0;

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

        _startBtn = new Button(Resources.Load<Sprite>("StartBtn"), Resources.Load<Sprite>("StartBtnDown"), GameObject.Find("StartBtn"));
        _instrBtn = new Button(Resources.Load<Sprite>("InstructBtn"), Resources.Load<Sprite>("InstructBtnDown"), GameObject.Find("InstructBtn"));
        _exitBtn = new Button(Resources.Load<Sprite>("ExitBtn"), Resources.Load<Sprite>("ExitBtnDown"), GameObject.Find("ExitBtn"));

        _startBtn.changeState(Button.BUTTON_DOWN);
        _instrBtn.changeState(Button.BUTTON_UP);
        _exitBtn.changeState(Button.BUTTON_UP);
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

    void changeSelected(int option)
    {
        switch(option)
        {
            case 0:
                _startBtn.changeState(Button.BUTTON_DOWN);
                _instrBtn.changeState(Button.BUTTON_UP);
                _exitBtn.changeState(Button.BUTTON_UP);
                break;
            case 1:
                _startBtn.changeState(Button.BUTTON_UP);
                _instrBtn.changeState(Button.BUTTON_DOWN);
                _exitBtn.changeState(Button.BUTTON_UP);
                break;
            case 2:
                _startBtn.changeState(Button.BUTTON_UP);
                _instrBtn.changeState(Button.BUTTON_UP);
                _exitBtn.changeState(Button.BUTTON_DOWN);
                break;
            default:
                _startBtn.changeState(Button.BUTTON_UP);
                _instrBtn.changeState(Button.BUTTON_UP);
                _exitBtn.changeState(Button.BUTTON_UP);
                break;
        }
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

            changeSelected(_curOption);
        }

        if(isPressed(KeyCode.DownArrow))
        {
            if(++_curOption > 2)
            {
                _curOption = 0;
            }

            changeSelected(_curOption);
        }

        if(isPressed(KeyCode.Z))
        {
            switch(_curOption)
            {
                case 0:
                    //TODO: Switch scene
                    break;

                case 1:
                    //TODO: Display instrucitons
                    break;

                case 2:
                    Application.Quit();
                    break;
            }
        }

        if(isPressed(KeyCode.X))
        {
            _curOption = 0;
            changeSelected(_curOption);
        }

        resetInput();
	}
}
