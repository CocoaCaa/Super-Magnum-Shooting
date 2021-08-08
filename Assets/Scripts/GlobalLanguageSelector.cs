using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class GlobalLanguageSelector : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;

        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            options.Add(new TMP_Dropdown.OptionData(locale.LocaleName));
        }
        dropdown.options = options;

        string playerLocaleName = PlayerPrefs.GetString("locale", LocalizationSettings.AvailableLocales.Locales[0].name);
        int playerLocaleIdx = LocalizationSettings.AvailableLocales.Locales.FindIndex(0, (l) => l.name == playerLocaleName);
        playerLocaleIdx = playerLocaleIdx == -1 ? 0 : playerLocaleIdx;
        dropdown.value = playerLocaleIdx;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[playerLocaleIdx];
    }

    public void HandleOnChangeLanguage(int idx)
    {
        var selectLocale = LocalizationSettings.AvailableLocales.Locales[idx];
        LocalizationSettings.SelectedLocale = selectLocale;
        PlayerPrefs.SetString("locale", selectLocale.name);
    }
}
