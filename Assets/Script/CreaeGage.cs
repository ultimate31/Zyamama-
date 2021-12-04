using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreaeGage : MonoBehaviour
{
    [Header("�ǂ��Ώ�(���m)")]
    [SerializeField]
    private Transform targetTfm;

    private RectTransform myRectTfm;//���̉摜�̍��W

    [Header("��Ǝ���")]
    public float CreateTime;

    [Header("�Q�[�W�̉摜")]
    [SerializeField]
    private Image GageImage;

    [Header("�Q�[�W�̉摜(�w�i)")]
    [SerializeField]
    private Image BuckGage;

    private Vector3 offset = new Vector3(0, 1.5f, 0);

    private bool Downnow;

    GameObject Doctor;

    void Start()
    {
        myRectTfm = GetComponent<RectTransform>();//���݂̍��W��ۑ�
        GageImage.fillAmount = BuckGage.fillAmount = 0;//�摜�����Z�b�g(��\��)
        Doctor = GameObject.Find("Doctor");
    }

    void Update()
    {
        //�^�[�Q�b�g��ǂ�
        myRectTfm.position�@= RectTransformUtility.WorldToScreenPoint(Camera.main, targetTfm.position + offset);

        //�Q�[�W�摜�ɍ�Ǝ��Ԃ𔽉f������
        GageImage.fillAmount = Doctor.GetComponent<DoctorManager>().Createnow_Time / Doctor.GetComponent<DoctorManager>().Create_Time;
        
        if (GageImage.fillAmount > 0)
        {
            BuckGage.fillAmount = 1;
        }
        else
        {
            BuckGage.fillAmount = 0;
        }
    }
}