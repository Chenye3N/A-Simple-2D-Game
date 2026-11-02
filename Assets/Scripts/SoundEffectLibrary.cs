using UnityEngine;
using System.Collections.Generic;
public class SoundEffectLibrary:MonoBehaviour{
    private Dictionary<string,List<AudioClip>>soundEffectLibrary;
    [SerializeField] private SoundEffectGroup[] soundEffectGroups;
    private void Awake(){
        InitializeLibrary();
    }
    private void InitializeLibrary(){
        soundEffectLibrary=new Dictionary<string,List<AudioClip>>();
        foreach(SoundEffectGroup group in soundEffectGroups){
            soundEffectLibrary.Add(group.name,group.soundEffects);
        }
    }
    public AudioClip GetRamdomSOundEffect(string name){
        if(soundEffectLibrary.ContainsKey(name)){
            List<AudioClip>soundEffects=soundEffectLibrary[name];
            if(soundEffects.Count>0){
                int randomIndex=Random.Range(0,soundEffects.Count);
                return soundEffects[randomIndex];
            }
            else{
                Debug.LogWarning($"Sound effect group '{name}' is empty.");
                return null;
            }
        }
        Debug.LogWarning($"Sound effect group '{name}' not found.");
        return null;
    }
}

[System.Serializable]
public struct SoundEffectGroup{
    public string name;
    public List<AudioClip>soundEffects;

}