# 🕹️ Micro Doctor  

> XR 특별 교육 과정에서 진행한 2인 팀 프로젝트 (개발 기간: 약 **3개월**)  
> 장르: **뱀서라이크(Vampire Survivors-like)**  
> 스토리: 플레이어는 의사가 되어 환자의 몸속으로 들어가 병균을 무찌르고 수술을 성공시켜야 합니다.  

---

## 📌 프로젝트 개요
- **프로젝트명**: Micro Doctor  
- **진행 인원**: 2명 (본인 포함)  
- **개발 기간**: 약 3개월  
- **개발 환경**: Unity, C#  
- **플랫폼**: PC  
- **장르**: 뱀서라이크  

---

## 🎮 주요 학습 내용
- **오브젝트 풀링(Object Pooling)**: 지속적인 몬스터 생성 과정에서 성능 최적화를 위해 적용  
- **오클루전 컬링(Occlusion Culling) 간접 적용**: 카메라 시야 밖의 몬스터 렌더링을 비활성화하는 방식으로 구현  
- **애니메이션 및 UI 연출**: **Dotween**을 활용하여 UI 애니메이션 및 연출 경험 축적  

---

## 📰 담당 업무  

### 1. 게임 컨텐츠 제작
- **Player**  
  - `GetAxis` 기반 기본 이동 로직 구성  
  - `Spacebar` 대쉬 기능 추가 (RigidBody의 Velocity 가속 활용)  

- **Spawner**  
  - 카메라 기준 꼭짓점과 면마다 총 20개의 몬스터 스포너 배치  
  - Timer & SpawnTime 변수를 통해 몬스터 스폰 주기 점진적 감소 → 난이도 조정  

- **TileMap**  
  - 노이즈 기반 랜덤 배경 배치 및 장애물 생성  
  - 카메라 벗어날 시 맵을 이동시켜 **무한 맵처럼 보이도록 구현**  

- **오브젝트 풀링**  
  - 성능 최적화를 위해 몬스터를 초기 생성 시 풀링 후 재사용  

- **오클루전 컬링 (간접 적용)**  
  - 카메라 밖으로 벗어난 몬스터 → 렌더링 비활성화  
  - 다시 시야에 들어오면 렌더링 재활성화  

---

### 2. UI
- **메인화면**: 무기 도감 구현 → 사전 무기 조합 가능  
- **캐릭터 선택창**: 해금 조건 표시 (PlayerPrefs 카운팅 활용)  
- **인게임 HUD**  
  - 상단: 경험치바, 진행 시간  
  - 좌상단: 선택 무기 & 궁극기  
  - 최소 UI 배치로 가독성 확보  

---

## 📸 스크린샷 (예시)
> <img width="866" height="484" alt="image" src="https://github.com/user-attachments/assets/7da2b06c-9801-4464-8c33-50d0787ef486" />

> <img width="1918" height="1078" alt="image" src="https://github.com/user-attachments/assets/da1e4893-c0db-459a-845b-9a6755db2d8f" />

> <img width="1892" height="1080" alt="image" src="https://github.com/user-attachments/assets/dd070be7-c05a-4679-bfee-ed9659ca3a45" />

> <img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/12cbd7a8-1e41-4c84-9ea6-cbcc355807bd" />


---

## 🚀 배운 점
- 오브젝트 풀링을 통한 **성능 최적화 경험**  
- 오클루전 컬링 개념 및 렌더링 최적화 이해  
- Dotween 기반의 **UI 연출 경험**  
- 레벨 디자인 및 무한 맵 구현 방법 습득  
