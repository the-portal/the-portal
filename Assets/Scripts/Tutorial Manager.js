import UnityEngine.UI;

var newPage : GUITexture[];
var screenNum : int = 0;
private var ratio : float = 0.725;
private var threshold : float = Screen.width * 0.1;
private var touchstartx : float;
 
var startpos : float;
var swipedistance : float;
var direction : boolean;
var target : float;

var indicator : GUITexture[];
var space : float;
var smoothtime : float = 0.5;
private var yVelocity = 0.0;
var halfnumber : int;

var indicatorUp : Texture;
var indicatorDown : Texture;
var Skip_btn : GameObject;

 function Start () 
 {
 	indicator[0].texture = indicatorUp;
 	pageSlider();
 	PageIndicator();
 }
 
 function Update () 
 {	
     
    for(var touch in Input.touches)
	{
		if(touch.phase == TouchPhase.Began)
    	{
    		touchstartx = touch.position.x;
   	    }
   	    
		if(touch.phase == TouchPhase.Moved)
    	{

   	    }
   	    
   	    if(touch.phase == TouchPhase.Ended)
    	{
    		swipedistance =  touchstartx - touch.position.x;
    		if(swipedistance < 0)
    		{
    			swipedistance *=-1;
    		}
    		
    		if(swipedistance > threshold)
    		{
    			if(touchstartx > touch.position.x)
	    		{
	    			if(screenNum < newPage.Length-1)
	    			{
						direction = true;
	    				screenNum++;
						indicatorState();
	    			}
	    		} 
	    		else
	    		{
	    			if(screenNum > 0)
	    			{
	    				direction = true;
	    				screenNum--;
	    				indicatorState();
	    			}
	    		}
    			target = (startpos - newPage[screenNum].pixelInset.width * screenNum);
    		}
   	    }
    }

    if(screenNum == 2)
    {
        Skip_btn.SetActive(true);
    }
    else
    {
    	 Skip_btn.SetActive(false);
    }

	if(direction)
	{
	    newPage[0].pixelInset.x = Mathf.SmoothDamp(newPage[0].pixelInset.x,target,yVelocity,smoothtime * Time.deltaTime * 15);

	    
	}
	
	for(var y : int = 1; y < newPage.length;y++)
	{
		newPage[y].pixelInset.x = newPage[y - 1].pixelInset.x + newPage[y].pixelInset.width;
	}
}
function pageSlider()
{
	for(var i : int = 0 ; i < newPage.length; i++)
	{
		newPage[i].pixelInset.height = Screen.height;
		newPage[i].pixelInset.width = Screen.height * ratio;
		
		
		if(i == 0)
		{	
			startpos = ((newPage[i].pixelInset.width - Screen.width) /2) * -1;
			newPage[i].pixelInset.x = startpos;
		} 
		else
		{
			newPage[i].pixelInset.x = newPage[i-1].pixelInset.x + newPage[i].pixelInset.width;
		}
 	}
}

function PageIndicator()
{
	space = Screen.width/20;
	halfnumber = indicator.length/2;
	var midPoint : float = Screen.width/2;
	var btnWidth : float = Screen.height/38;
	var boolAdd : boolean;
	if(indicator.length % 2 != 0)
	{
		for(var i : int = 0 ; i < indicator.length; i++)
		{
			indicator[i].pixelInset.width = btnWidth;
			indicator[i].pixelInset.height = btnWidth;
			indicator[i].pixelInset.y = Screen.height * 0.07;
			
			if(!boolAdd)
			{
				indicator[i].pixelInset.x = (midPoint - (btnWidth/2)) - (btnWidth * (2 * halfnumber--));
				if(halfnumber == -1)
				{
					halfnumber =0;
					boolAdd = true;
				}
			}
			else
			{
				indicator[i].pixelInset.x = (midPoint + (btnWidth/2)) + (btnWidth * (1 + halfnumber));
				halfnumber +=2;
			}
		}
	}
	else
	{
		for(var t : int = 0 ; t < indicator.length; t++)
		{
			indicator[t].pixelInset.width = btnWidth;
			indicator[t].pixelInset.height = btnWidth;
			indicator[t].pixelInset.y = Screen.height * 0.07;
			
			if(!boolAdd)
			{
				indicator[t].pixelInset.x = ((midPoint - (btnWidth/2)) - (btnWidth * (2 * halfnumber--))) + btnWidth;
				if(halfnumber == -1)
				{
					halfnumber =0;
					boolAdd = true;
				}
			}
			else
			{
				indicator[t].pixelInset.x = ((midPoint + (btnWidth/2)) + (btnWidth * (1 + halfnumber))) + btnWidth;
				halfnumber +=2;
			}
		}
	}
			
}
function indicatorState()
{
	for(var i : int = 0; i < indicator.length;i++)
	{
		if(screenNum == i)
		{
			indicator[i].texture = indicatorUp;
		}
		else
		{
			indicator[i].texture = indicatorDown;
		}
	}
}
function Goto()
{
	Application.LoadLevel("SignUp LogIn");
}
