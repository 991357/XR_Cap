using UnityEngine;
using DG.Tweening;

public class KnifeController : MonoBehaviour
{
    private bool isAnimationPlaying = false; // �ִϸ��̼��� ���� �������� ��Ÿ���� �÷���

    private void Update()
    {
        // �ִϸ��̼��� ���� ���̸� �߰����� Raycast ����
        if (isAnimationPlaying)
            return;

        // �÷��̾�� ������ Į�� ��ġ
        Vector3 knifePosition = transform.position + new Vector3(0, -2, 0);

        // Į���� ���̸� �߻�
        RaycastHit2D hit = Physics2D.Raycast(knifePosition, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Enemy"));

        // Enemy�� ������ ���
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // A �Լ� ȣ��
            A(hit.collider.gameObject);
        }
    }

    private void A(GameObject enemy)
    {
        // �ִϸ��̼��� ���� ������ �÷��׷� ǥ��
        isAnimationPlaying = true;

        // DOTween�� ����Ͽ� Į�� Enemy���� �̵���Ű�� �ǵ��ƿ��� �ִϸ��̼� ����
        transform.DOMove(enemy.transform.position, 0.5f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // �̵��� ���� �Ŀ� ȣ��Ǵ� �κ�
                // �ǵ��ƿ��� �ִϸ��̼� ����
                transform.DOMove(transform.position + new Vector3(0, -2, 0), 0.5f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        // �� ��° �ִϸ��̼��� ������ ȣ��Ǵ� �κ�
                        // �̰��� �߰� �ִϸ��̼��̳� ������ ���� �� �ֽ��ϴ�.

                        // �ִϸ��̼��� �������� �÷��׷� ǥ��
                        isAnimationPlaying = false;

                        Debug.Log("Knife animation complete");
                    });
            });
    }
}