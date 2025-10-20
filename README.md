# 🎮 Snow 2D – PRU Project (FPT University HCM)

## 🧊 1. Giới thiệu

Snow 2D là trò chơi trượt tuyết 2D mặt ngang được phát triển bằng Unity cho môn học PRU – FPT University HCM.

Người chơi điều khiển một vận động viên trượt tuyết tự động di chuyển về phía trước trên địa hình tuyết được tạo ngẫu nhiên bằng Sprite Shape.  
Mục tiêu của người chơi là nhảy qua chướng ngại vật, ăn vật phẩm và tồn tại lâu nhất có thể.

---

## 🕹️ 2. Gameplay & Cơ chế điều khiển

### 🎯 Mục tiêu:

- Duy trì mạng sống (3 tim ban đầu).
- Thu thập vật phẩm để tăng điểm và sức mạnh.
- Tránh va chạm với người chơi khác hoặc đá.

### 🎮 Điều khiển:

| Hành động | Phím    | Mô tả                                 |
| --------- | ------- | ------------------------------------- |
| Nhảy      | `Space` | Nhảy qua chướng ngại vật hoặc vật cản |
| Tạm dừng  | `ESC`   | Tạm dừng trò chơi                     |

> ⚠️ Nhân vật tự động di chuyển về phía trước, người chơi chỉ điều khiển nút nhảy.

### 🧱 Cơ chế chính:

- Điểm số:
  - Mỗi đồng xu (coin) ăn được → +1 điểm.
- Sinh mệnh:
  - Người chơi có 3 tim ban đầu.
  - Khi va chạm người chơi khác → mất 1 tim.
  - Khi còn 0 tim → Game Over.
  - Có thể ăn vật phẩm tim ❤️ để hồi 1 tim.
- Tăng tốc (⚡ Tia sét):
  - Ăn được giúp tăng tốc độ tạm thời trong vài giây.
- Đá (🪨):
  - Khi chạm vào sẽ giảm tốc trong thời gian ngắn.

---

## ⚙️ 3. Kỹ thuật & Công nghệ sử dụng

| Thành phần         | Công nghệ / Công cụ                                               |
| ------------------ | ----------------------------------------------------------------- |
| Engine             | Unity 6+                                                          |
| Ngôn ngữ           | C#                                                                |
| Hệ thống vật lý    | Rigidbody2D, Collider2D                                           |
| Tạo địa hình tuyết | Sprite Shape Controller (Unity)                                   |
| Animation          | Animator Controller (Sprite-based)                                |
| Hiệu ứng           | Particle System (tuyết, va chạm, tăng tốc)                        |
| UI                 | Unity Canvas (HUD, điểm, tim, hiệu ứng)                           |
| Audio              | Unity AudioSource, AudioMixer (BGM & SFX)                         |
| Version Control    | Git + GitHub                                                      |
| Build Target       | Windows / WebGL                                                   |
| Tài liệu tham khảo | Unity Docs, YouTube (Brackeys, Game Maker’s Toolkit, Code Monkey) |

---

## 💻 4. Hướng dẫn cài đặt và chạy

### 📦 Yêu cầu hệ thống

- Unity Editor 6 trở lên
- .NET SDK (tích hợp trong Unity)
- RAM ≥ 8GB
- GPU hỗ trợ OpenGL 3.0 hoặc cao hơn

### 🚀 Cách chạy game

```bash
# Clone project từ GitHub
git clone https://github.com/PhuHVN/Lab2_PRU212

# Mở bằng Unity Hub
Unity Hub → Open Project → Chọn thư mục Lab2_PRU212

# Chạy Scene chính
Mở: Assets/Scenes/→ Nhấn Play
```

---

## 🧠 6. Logic Gameplay

- Game Manager:  
  Quản lý trạng thái game (Start, Playing, Game Over), điểm, số tim, tốc độ, tăng/giảm tốc.

- Player Controller:

  - Xử lý input (phím Space).
  - Quản lý va chạm, trọng lực, và animation.
  - Kích hoạt hiệu ứng tăng tốc / giảm tốc khi chạm vật phẩm.

- Item Spawner:

  - Sinh ngẫu nhiên vật phẩm: coin, tim, tia sét.
  - Tự điều chỉnh tần suất xuất hiện theo độ khó.

- Obstacle Controller:

  - Sinh và quản lý đá cản đường, va chạm giảm tốc.

- Sprite Shape Terrain Generator:
  - Tự động tạo đường tuyết ngẫu nhiên (mượt mà, có độ dốc thay đổi).
  - Cập nhật liên tục khi người chơi di chuyển.

---

## 🧭 7. Hệ thống UI/UX

| Thành phần       | Chức năng                                        |
| ---------------- | ------------------------------------------------ |
| Main Menu        | Bắt đầu game, thoát game                         |
| HUD              | Hiển thị điểm, số tim, tốc độ, hiệu ứng tăng tốc |
| Pause Menu       | Tạm dừng và tiếp tục                             |
| Game Over Screen | Hiển thị điểm đạt được và nút "Retry"            |

> UI được thiết kế đơn giản, trực quan, đảm bảo người chơi tập trung vào gameplay.  
> Các icon tim, đồng xu, tia sét được thể hiện rõ ràng, dễ nhận biết.

---

## 🚀 8. Mục tiêu phát triển & Roadmap

| Giai đoạn | Mục tiêu                                       | Trạng thái        |
| --------- | ---------------------------------------------- | ----------------- |
| Alpha     | Cơ chế di chuyển, nhảy, vật phẩm cơ bản        | ✅ Hoàn thành     |
| Beta      | Thêm hiệu ứng tăng/giảm tốc, HUD, âm thanh     | ✅ Hoàn thành     |
| Final     | Hoàn thiện UI, tối ưu gameplay, xuất bản WebGL | 🔄 Đang thực hiện |
| Future    | Thêm chế độ 2 người chơi hoặc leaderboard      | ⏳ Kế hoạch       |

---

## 🎥 9. Ảnh minh họa & Demo video

### 🎬 Video demo

👉 Link video demo: https://www.youtube.com/watch?v=dXZ1m-RFj6c

---

## 🎥 10. Dường dẫn game deploy

🎮 Link web game: https://phu04.itch.io/snow-skiiing

---

> ⚠️ Ghi chú quan trọng:  
> Dự án này được thực hiện chỉ phục vụ mục đích học tập trong khuôn khổ môn PRU – FPT University HCM.  
> Mọi tài nguyên (sprites, âm thanh, hiệu ứng) được sử dụng theo giấy phép miễn phí hoặc học thuật.
