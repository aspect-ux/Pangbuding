
public interface ISavable 
{
    /// <summary>
    /// C#8.0��֧��Ĭ�Ͻӿ�ʵ��
    /// </summary>
    void SavableRegister();

    GameSaveData SaveData();

    void RegisterGameData(GameSaveData gameSaveData);
}
