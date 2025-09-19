using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HoverButton : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Button button;           // Se autollenará si está vacío
    [SerializeField] private Graphic buttonGraphic;   // Por defecto, targetGraphic del botón
    [SerializeField] private Graphic textGraphic;     // <- Asigna AQUÍ el texto de ESTE botón
    [SerializeField] private RectTransform toAnimate; // Placeholder animación (opcional)

    [Header("Colors (hex)")]
    [SerializeField] private string normalHex      = "#FFFFFFFF";
    [SerializeField] private string highlightedHex = "#FFD54FFF";
    [SerializeField] private string pressedHex     = "#FFB300FF";

    //[Header("Scene")]
    //[SerializeField] private string sceneToLoad = "YourSceneName";

    private bool _isPointerOver;

    void Awake()
    {
        if (button == null) button = GetComponent<Button>();
        if (buttonGraphic == null && button != null) buttonGraphic = button.targetGraphic;

        if (button != null) button.transition = Selectable.Transition.None;

        ApplyColor(normalHex);
    }

    // ---------- Hover ----------
    public void OnPointerEnter(PointerEventData eventData)
    {
        _isPointerOver = true;
        ApplyColor(highlightedHex);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isPointerOver = false;
        ApplyColor(normalHex);
    }

    // ---------- Press ----------
    public void OnPointerDown(PointerEventData eventData)
    {
        ApplyColor(pressedHex);
        AnimateFocusPlaceholder(); // solo stub
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ApplyColor(_isPointerOver ? highlightedHex : normalHex);
    }

    /*public void OnPointerClick(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
            SceneManager.LoadScene(sceneToLoad);
        else
            Debug.LogWarning("HoverButton: 'sceneToLoad' está vacío en " + gameObject.name);
    }*/

    private void ApplyColor(string hex)
    {
        if (!ColorUtility.TryParseHtmlString(hex, out var c))
        {
            Debug.LogWarning($"HoverButton: Hex inválido '{hex}'. Usando blanco.");
            c = Color.white;
        }
        if (buttonGraphic != null) buttonGraphic.color = c;
        if (textGraphic   != null) textGraphic.color   = c;
    }

    // Placeholder para tu animación de “agrandar y centrar”
    private void AnimateFocusPlaceholder()
    {
        if (toAnimate == null) return;
        // TODO: guardar tamaño/posición originales y animar a centro/tamaño mayor.
    }
    
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
