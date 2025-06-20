using UnityEngine;

public class UI_Manager : Singleton<UI_Manager>
{
    [SerializeField] private UI_Board _postBoardPanel;
    [SerializeField] private UI_Board _postDetailPanel;
    [SerializeField] private UI_Board _postWritePanel;
    [SerializeField] private UI_Board _postModifyPanel;

}
