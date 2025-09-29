using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Member
{
    public string name;
    public string mssv;
}

public class MemberListController : MonoBehaviour
{
    public RectTransform contentParent;       // Content
    public GameObject memberItemTemplate;     // MemberItemTemplate có sẵn trong Scene
    public List<Member> members = new List<Member>();

    void Start()
    {
        // Xóa các bản copy cũ, nhưng giữ lại template
        foreach (Transform t in contentParent)
        {
            if (t.gameObject != memberItemTemplate)
                Destroy(t.gameObject);
        }

        // Tạo bản copy từ template
        foreach (var m in members)
        {
            GameObject go = Instantiate(memberItemTemplate, contentParent);
            go.SetActive(true); // phòng khi bạn tắt template
            var txt = go.GetComponent<Text>();
            if (txt != null) txt.text = $"{m.name} - {m.mssv}";
        }

        // Ẩn template gốc để không thấy trên UI
        memberItemTemplate.SetActive(false);
    }
}
