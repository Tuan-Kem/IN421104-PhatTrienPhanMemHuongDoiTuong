# User Stories — EduHub

Viết ít nhất 5 User Story, đủ 3 vai trò (Sinh viên, Ban CLB, Admin).
Mỗi story có ít nhất 2 tiêu chí nghiệm thu (Given/When/Then hoặc gạch đầu dòng).

## US-01 · Sinh viên: <việc cần làm>

Là [vai trò], tôi muốn [việc cần làm], để [lợi ích].

- Tiêu chí 1: Given ... When ... Then ...
- Tiêu chí 2: Given ... When ... Then ...

Là sinh viên, tôi muốn tham gia một CLB, để cải thiện các kỹ năng và hoàn thiện bản thân.

- Given chưa tham gia CLB, when tham gia CLB Then có tên trong `club.Members`.
- Given đã tham gia CLB, When tham gia CLB, Then Không thể tham gia.

## US-02 · Điểm danh (Check-in) sự kiện
Là Sinh viên, tôi muốn thực hiện check-in khi đến tham dự sự kiện, để được ghi nhận sự có mặt thực tế trên hệ thống.
- Given phiếu đăng ký đã được chuyển sang trạng thái Confirmed, When tôi tiến hành check-in, Then hệ thống xác nhận thành công và tạo bản ghi CheckIn.
- Given phiếu đăng ký vẫn đang ở trạng thái Pending hoặc đã Cancelled, When tôi cố gắng check-in, Then hệ thống đưa ra cảnh báo không hợp lệ và từ chối ghi nhận.
### Ban CLB

## US-03 · Tạo mới SK
Là Ban CLB, tôi muốn khởi tạo một sự kiện mới trên hệ thống, để mở đợt đăng ký và thu hút các bạn sinh viên tham gia.

- Given tên sự kiện hợp lệ và có Capacity > 0, When tôi bấm tạo sự kiện, Then sự kiện mới được khởi tạo và gán chính xác ClubId tương ứng.

- Given tiêu đề sự kiện bị bỏ trống hoặc thiết lập Capacity <= 0, When tôi bấm tạo sự kiện, Then hệ thống báo lỗi dữ liệu nhập không hợp lệ.

## US-04 · Theo dõi báo cáo tham dự
Là Ban CLB, tôi muốn truy xuất báo cáo thống kê tình hình tham gia, để đánh giá hiệu quả tổ chức và tỷ lệ có mặt thực tế của sinh viên.

- Given sự kiện đã mở đăng ký và điểm danh, When Ban CLB mở màn hình báo cáo, Then hệ thống hiển thị rõ thông số tổng số lượt đăng ký trên tổng sức chứa và tổng số sinh viên đã hoàn thành check-in.

- Given sự kiện có danh sách đăng ký, When Ban CLB xem báo cáo chi tiết, Then hệ thống liệt kê cụ thể danh sách từng sinh viên kèm trạng thái đã check-in hay chưa điểm danh.
### Admin
## US-05 · Ngừng hoạt động thành viên
Là Admin, tôi muốn tắt trạng thái hoạt động của một thành viên, để đảm bảo danh sách thành viên trong CLB luôn chính xác và đúng thực tế.

- Given thành viên đang có trạng thái Active, When tôi thực hiện chuyển trạng thái ngừng hoạt động, Then phương thức IsActive() sẽ trả về giá trị false.

- Given thành viên bị chuyển sang trạng thái ngừng hoạt động, When Admin hoặc hệ thống tra cứu lại dữ liệu, Then toàn bộ lịch sử đăng ký sự kiện từ trước đến nay của thành viên đó vẫn được bảo toàn nguyên vẹn trong hệ thống.

<!-- TODO: thêm US-02 đến US-05 (hoặc nhiều hơn) -->