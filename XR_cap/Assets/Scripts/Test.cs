using UnityEngine;
using DG.Tweening;

public class KnifeController : MonoBehaviour
{
    private bool isAnimationPlaying = false; // 애니메이션이 실행 중인지를 나타내는 플래그

    private void Update()
    {
        // 애니메이션이 실행 중이면 추가적인 Raycast 방지
        if (isAnimationPlaying)
            return;

        // 플레이어에서 떨어진 칼의 위치
        Vector3 knifePosition = transform.position + new Vector3(0, -2, 0);

        // 칼에서 레이를 발사
        RaycastHit2D hit = Physics2D.Raycast(knifePosition, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Enemy"));

        // Enemy를 감지한 경우
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // A 함수 호출
            A(hit.collider.gameObject);
        }
    }

    private void A(GameObject enemy)
    {
        // 애니메이션이 실행 중임을 플래그로 표시
        isAnimationPlaying = true;

        // DOTween을 사용하여 칼을 Enemy에게 이동시키고 되돌아오는 애니메이션 설정
        transform.DOMove(enemy.transform.position, 0.5f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                // 이동이 끝난 후에 호출되는 부분
                // 되돌아오기 애니메이션 설정
                transform.DOMove(transform.position + new Vector3(0, -2, 0), 0.5f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        // 두 번째 애니메이션이 끝나면 호출되는 부분
                        // 이곳에 추가 애니메이션이나 로직을 넣을 수 있습니다.

                        // 애니메이션이 끝났음을 플래그로 표시
                        isAnimationPlaying = false;

                        Debug.Log("Knife animation complete");
                    });
            });
    }
}