
public interface ISavable 
{
    /// <summary>
    /// C#8.0才支持默认接口实现
    /// </summary>
    void SavableRegister();

    GameSaveData SaveData();

    void RegisterGameData(GameSaveData gameSaveData);
}
