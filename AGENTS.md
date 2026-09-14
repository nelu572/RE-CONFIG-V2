# RE:CONFIG-V2

`RE:CONFIG-V2`는 원작에서 출발해 새롭게 만드는 Unity 2D 퍼즐 게임이다. 플레이어는 SETTINGS를 조작해 월드의 규칙을 바꾸고, 각 ROOM의 EXIT에 도달한다. 원작의 ROOM·기믹·구현은 참고 자료일 뿐, 충실한 이식이 목표는 아니다.

## V1 기준 저장소

사용자가 `V1`, `원작`, `기존 C++판`이라고 말하면 [nelu572/RE-CONFIG](https://github.com/nelu572/RE-CONFIG)를 뜻한다. 해당 저장소는 V2의 참고 자료이며, V2의 요구사항이나 구현 방향을 자동으로 결정하지는 않는다.

## V2 게임 방향

- 핵심 진행은 반사신경, 정확한 점프, 타이밍 수행보다 규칙 관찰, 원인 추론, 계획, SETTINGS 조작으로 해결한다.
- ROOM은 실패해도 원인과 규칙의 결과를 읽을 수 있게 만들며, 피지컬 숙련이 퍼즐의 핵심 해법을 가로막지 않게 한다.
- 새 기믹과 ROOM은 원작의 구성에 맞추기보다 이 방향에 맞는 퍼즐·연출을 우선한다.

## 씬 구조

- `Assets/Scenes/_Development/Dev_Gameplay.unity`: 공용 게임플레이와 기믹의 개발·실험 씬
- `Assets/Scenes/Rooms/Room_XX.unity`: 실제 플레이 가능한 ROOM 단위 씬

아직 만들어지지 않은 씬·폴더는 첫 사용 작업에서 Unity Editor로 생성한다. 콘텐츠 씬의 영속적 배치와 참조는 런타임 코드가 아니라 에셋에 저장한다.

## 작업 원칙

- Unity 씬·프리팹·에셋 작업은 [.docs/harness/UNITY.md](.docs/harness/UNITY.md)를 먼저 읽는다.
- 코드·씬·에셋 변경 후 검증을 요청받거나 커밋·PR 전에 검증할 때는 [.docs/skills/UNITY_VERIFY.md](.docs/skills/UNITY_VERIFY.md)를 먼저 읽는다.
- 사용자가 커밋을 명시적으로 요청하면, stage·commit 전에 반드시 [.docs/skills/AUTO_COMMIT.md](.docs/skills/AUTO_COMMIT.md)를 읽고 절차와 메시지 규칙을 따른다.
- GitHub PR, 라벨, 이슈 관련 작업은 [.docs/harness/GITHUB.md](.docs/harness/GITHUB.md)를 먼저 읽는다. 사용자가 PR 생성을 명시적으로 요청하면, 생성 전에 반드시 [.docs/skills/AUTO_PR.md](.docs/skills/AUTO_PR.md)를 추가로 읽는다.
