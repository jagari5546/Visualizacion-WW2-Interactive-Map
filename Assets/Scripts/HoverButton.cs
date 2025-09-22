using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Selectable))]
public class CopyTintToText : MonoBehaviour
{
    [Header("Fuentes")]
    [SerializeField] private Graphic sourceGraphic; // si lo dejas vacío usa el targetGraphic del Selectable
    [Header("Destino")]
    [SerializeField] private Graphic textGraphic;   // Text o TextMeshProUGUI
    [SerializeField] private bool copyAlpha = true; // si no quieres copiar alpha, pon false

    Color _last;

    void Awake()
    {
        var sel = GetComponent<Selectable>();
        if (sourceGraphic == null) sourceGraphic = sel.targetGraphic;
        if (sourceGraphic != null) _last = sourceGraphic.color;
    }

    void LateUpdate()
    {
        if (sourceGraphic == null || textGraphic == null) return;

        var c = sourceGraphic.color;
        if (!copyAlpha) c.a = textGraphic.color.a;
        if (!NearlyEqual(c, _last))
        {
            textGraphic.color = c;
            _last = c;
        }
    }

    static bool NearlyEqual(Color a, Color b, float e = 0.001f)
    {
        return Mathf.Abs(a.r-b.r)<e && Mathf.Abs(a.g-b.g)<e &&
               Mathf.Abs(a.b-b.b)<e && Mathf.Abs(a.a-b.a)<e;
    }

    public void SceneChange(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}