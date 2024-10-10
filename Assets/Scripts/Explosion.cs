using UnityEngine;

public class Explosion : SoundManager
{
    private Animator animator;

    void Start()
    {


        PlaySound(sounds[0]);
        // ���������� ������ ����� 1 ������� ����� ������ ��������
        Destroy(gameObject, 0.5f); // ��������� ����� � ����������� �� ����� ����� ��������
    }
}
