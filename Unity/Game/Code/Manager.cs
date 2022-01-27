using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JamCat.UI;
using JamCat.Players;
using JamCat.Cameras;
using JamCat.Map;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    public static Manager Get() { return instance; }

    // Methods
    public Sys[] systems;
    public Data data;

    public bool dev_tools = true;
    public bool hacks = false;

    public int frontModule = 0;
    public float curDist = 100000.0f;

    // Methods -> Standard
    private void Awake() {
        instance = this;
        data.OnAwake();
        for (int i = 0; i < systems.Length; i++)
            systems[i].AwakeSys();
    }

    private void Start() {
        for (int i = 0; i < systems.Length; i++)
            systems[i].StartSys();

        Window_Options.Get().LoadAndApply();
        GeneralMethods.MainMenu();
    }

    private void Update() {
        for (int i = 0; i < systems.Length; i++)
            systems[i].UpdateSys();


        if (dev_tools == true) {
            Dev_InputToSkip();
        }

        UpdateFirstCar();
        UpdateDeaths();
    }

    public void UpdateFirstCar(){
        List<JamCat.Players.Player> onPlayers = systems[1].GetComponent<SysPlayer>().onlinePlayers;
        for(int i = 0; i<onPlayers.Count; i++){
            if(onPlayers[i].inModule > frontModule){
                systems[2].GetComponent<SysCamera>().SetPlayerTarget(onPlayers[i].transform);
                frontModule = onPlayers[i].inModule;
                curDist = 20000.0f;
            }
            if(onPlayers[i].inModule == frontModule){
                if(CheckCloser(onPlayers[i])){
                    systems[2].GetComponent<SysCamera>().SetPlayerTarget(onPlayers[i].transform);
                }
            }
        }
    }

    public void UpdateDeaths(){
        List<JamCat.Players.Player> onPlayers = systems[1].GetComponent<SysPlayer>().onlinePlayers;
        for(int i = 0; i<onPlayers.Count; i++){
            if(CheckDeath(onPlayers[i])){
                RestartOnModule();
            }

        }
    }

    public bool CheckDeath(Player pl){
        Vector3 cam = systems[2].GetComponent<SysCamera>();
        if(IsObjectVisible(cam, pl.GetCoponent<SpriteRenderer>())){
            return false;
        }
        else{
            return true;
        }
        
    }

    public bool CheckCloser(Player pl){
        Module mod = systems[3].GetComponent<SysMap>().generatorServer.GetModuleCreated(pl.inModule);
        Vector3 mod_pos = mod.transform.position;
        Vector3 offset = new Vector3(mod.moduleConfiguration.to_direction.x * mod.moduleConfiguration.size, mod.moduleConfiguration.to_direction.y * mod.moduleConfiguration.size,0);
        if(Vector3.Distance(mod_pos+offset, pl.transform.position)<curDist){
            curDist = Vector3.Distance(mod_pos+offset, pl.transform.position);
            return true;
        }
        return false;
    }

    public void RestartOnModule(){
        List<JamCat.Players.Player> onPlayers = systems[1].GetComponent<SysPlayer>().onlinePlayers;
        Module mod = systems[3].GetComponent<SysMap>().generatorServer.GetModuleCreated(pl.inModule);
        Vector3 mod_pos = mod.transform.position;
        Vector3 offset = new Vector3(mod.moduleConfiguration.to_direction.x * mod.moduleConfiguration.size, mod.moduleConfiguration.to_direction.y * mod.moduleConfiguration.size,0);
        for(int i = 0; i<onPlayers.Count; i++){
            onPlayers[i].transform.position = mod_pos - 1/2 * offset;
        }
    }

    public bool IsObjectVisible(JamCat.Cameras.SysCamera cam, Renderer renderer)
    {
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(cam.GetComponent<CameraFollowCar>()), renderer.bounds);
    }

    public void Dev_InputToSkip() {
        if (Input.GetKeyDown(KeyCode.O))
            SkipToGame(0);
            /*
        } else if (Input.GetKeyDown(KeyCode.Keypad2)) {
            SkipToGame(1);
        } else if (Input.GetKeyDown(KeyCode.Keypad3)) {
            SkipToGame(2);
        } else if (Input.GetKeyDown(KeyCode.Keypad4)) {
            SkipToGame(3);
        }
        */
    }

    public void SkipToGame(int characterNumber) {

        if (Data.Get().gameLogic.in_main_menu == true) {
            Window_MainMenu.Get().Button_Play();
            Window_Lobby.Get().ButtonHost();
            Window_CharacterSelection.Get().ButtonSelectCharacter(characterNumber);
            Window_CharacterSelection.Get().ToggleReady();
        }
        
        if (Data.Get().gameLogic.game_finished == true) {
            Window_Finish.Get().Button_PlayAgain();
        }
        
        if (Data.Get().gameLogic.in_game == true) {
            GeneralMethods.StartGame();
        }
        
        
       // GeneralMethods.StartGame();
    }
}