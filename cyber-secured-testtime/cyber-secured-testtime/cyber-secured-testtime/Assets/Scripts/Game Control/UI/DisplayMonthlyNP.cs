/// FILE: DisplayMonthNP.cs
/// PURPOSE: A script to update the total score.
/// 
/// FUNCTIONS:
///
/// void OnMouseEnter()
///     When the mouse enters the BoxCollider2D region of the object,
///     in this case the object would be the text containing score,
///     update the text to display the correct monthly_np in GameControllerV2,
///     and activate the display box containing the text
/// 
/// void OnMouseOver()
///     When the mouse is hovered over ^,
///     update the position of the small box containing the monthly_np text
///     following the mouse position
/// 
/// void OnMouseExit()
///     When the mouse leaves the area ^,
///     deactivate the display box containing the text
///

using UnityEngine;
using UnityEngine.UI;

public class DisplayMonthlyNP : MonoBehaviour
{
    public GameObject panel_monthly_np;
    public Text text_monthly_np;

    void OnMouseEnter()
    {
        //if a company has been chosen yet
        if(GameControllerV2.Instance.CHOSEN_COMPANY != GameControllerV2.Company.none)
        {
            text_monthly_np.text = "Monthly NP: +" + GameControllerV2.Instance.GetMonthlyNP();
        }

        panel_monthly_np.SetActive(true);
    }

    void OnMouseOver()
    {
        //TODO: figure out why this is so wonky - the need to offset the value
        float mouse_x = Input.mousePosition.x - 480;
        float mouse_y = Input.mousePosition.y - 380;

        panel_monthly_np.transform.localPosition = new Vector2(mouse_x, mouse_y);
    }

    void OnMouseExit()
    {
        panel_monthly_np.SetActive(false);
    }
}
