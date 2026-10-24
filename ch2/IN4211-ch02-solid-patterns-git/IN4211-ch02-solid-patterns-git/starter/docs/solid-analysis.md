# 1.  Vi phạm Single Responsibility Principle (SRP)Vị trí: Phương thức CreateClub (dòng 27–57) và AddMemberToClub (dòng 60–77). 

Một class chỉ nên có một lý do duy nhất để thay đổi.

CreateClub ôm 4 công việc khác nhau: kiểm tra hợp lệ dữ liệu , lưu CLB vào danh sách, ghi log ra file text, và gửi thông báo email. AddMemberToClub vừa kiểm tra giới hạn thành viên, vừa lưu dữ liệu, vừa ghi log file, vừa in ra thông báo email lẫn Slack.   

Làm code dài, rối rắm, khó đọc và khó viết Unit Test độc lập cho logic nghiệp vụ.

Hướng sửa: Tách ClubManager thành các lớp riêng biệt   
    ClubService: Chỉ điều phối logic nghiệp vụ.   
    IClubRepository: Quản lý lưu trữ dữ liệu.   
    IAppLogger: Đảm nhận ghi log.   
    INotificationService: Đảm nhận gửi thông bo.  


2. Vi phạm Open/Closed Principle (OCP)Vị trí: Phương thức CreateClub (dòng 43–56).  

Mở rộng tính năng bằng cách thêm mới, không chỉnh sửa code cũ đã hoạt động. 

Dùng chuỗi if/else để kiểm tra tên miền email (@gmail.com, @yahoo.com). Khi cần thêm kênh thông báo mới (như SMS, Slack, Zalo), buộc phải mở phương thức CreateClub ra sửa và thêm nhánh else if. Điều này dễ sinh ra bug cho các tính năng cũ.   

Hướng sửa:Tạo interface INotificationService với phương thức Send(to, message).Mở rộng bằng cách tạo thêm lớp cài đặt mới (EmailNotificationService, SmsNotificationService, SlackNotificationService).   Sử dụng Factory Pattern (NotificationServiceFactory) để tự động chọn kênh thông báo.   

3. Vi phạm Dependency Inversion Principle (DIP)Vị trí: Các phương thức CreateClub (dòng 40), AddMemberToClub (dòng 72), CreateEvent (dòng 85), và PrintClubReport (dòng 102).  

Phụ thuộc vào abstraction (interface), không phụ thuộc vào class cụ thể hoặc thư viện I/O cứng.   

Gọi trực tiếp phương thức tĩnh File.AppendAllText(_logFilePath, ...) và Console.WriteLine(...) bên trong nghiệp vụ. Code bị dính chặt với hệ thống file thật và Console. Khi chạy Unit Test, test case sẽ bị chậm, phụ thuộc vào ổ đĩa và khó thay thế bằng logger giả (Mock/Stub).   

Hướng sửa: Thay thế việc gọi File hoặc Console bằng interface IAppLogger. Tiêm IAppLogger qua constructor (Constructor Injection) của service.   

4. Vi phạm Open/Closed Principle (OCP) trong xuất báo cáoVị trí: Phương thức PrintClubReport (dòng 95–104).   

Mở rộng tính năng bằng cách thêm mới, không chỉnh sửa code cũ.   

Phương thức in cứng dữ liệu báo cáo ra màn hình Console. Nếu hệ thống muốn xuất báo cáo theo các định dạng khác như CSV, PDF, hoặc JSON, lập trình viên bắt buộc phải sửa trực tiếp hàm PrintClubReport.   

Hướng sửa: Áp dụng Strategy Pattern.   Tạo interface IReportStrategy với phương thức Render(stats).   Định nghĩa các lớp chiến lược riêng biệt: ConsoleReportStrategy, CsvReportStrategy.   

5. Vi phạm Single Responsibility Principle (SRP) & Coupling trong tạo sự kiệnVị trí: Phương thức CreateEvent (dòng 80–90).   

Mỗi class chỉ làm một việc và giảm bớt sự phụ thuộc chặt chẽ giữa các đối tượng.   

CreateEvent vừa tạo đối tượng Event, vừa tự thực hiện vòng lặp foreach duyệt danh sách _members để gửi email. Mỗi khi có nhu cầu thêm bên thứ ba muốn nhận tin khi có sự kiện mới (ví dụ: LogNotifier, SMSNotifier), phương thức CreateEvent phải sửa đổi.   

Hướng sửa:Áp dụng Observer Pattern. EventService chỉ làm nhiệm vụ tạo sự kiện và phát tín hiệu cho các IEventObserver đã đăng ký (như EmailNotifier, LogNotifier).