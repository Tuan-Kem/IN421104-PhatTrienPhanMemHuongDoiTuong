using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EduHub.Ch01.Domain;

namespace EduHub.Ch01.ConsoleDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            const string separator = "========================================";
            const int capacity = 50;

            Console.WriteLine(separator);
            Console.WriteLine("    EDUHUB - CLUB & EVENT MANAGEMENT    ");
            Console.WriteLine(separator);

            // 1. Tạo câu lạc bộ
            Console.WriteLine("\n1. Tạo câu lạc bộ");
            var club = Club.Create(
                "Tech Club",
                "Câu lạc bộ công nghệ và lập trình sinh viên"
            );

            Console.WriteLine($"Tên CLB: {club.Name}");
            Console.WriteLine($"Mô tả: {club.Description}");

            // 2. Thêm thành viên
            Console.WriteLine("\n2. Thêm thành viên");

            var m1 = Member.Create("Nguyễn Văn An", "an.nv@student.dthu.edu.vn", "DH24001");
            var m2 = Member.Create("Trần Thị Bình", "binh.tt@student.dthu.edu.vn", "DH24002");
            var m3 = Member.Create("Lê Hoàng Cường", "cuong.lh@student.dthu.edu.vn", "DH24003");

            var members = new List<Member> { m1, m2, m3 };

            foreach (var member in members)
            {
                club.AddMember(member);
                Console.WriteLine($"- {member.Name} ({member.StudentCode})");
            }

            Console.WriteLine($"Tổng thành viên: {club.MemberCount()}");

            // 3. Tạo sự kiện
            Console.WriteLine("\n3. Tạo sự kiện");

            var workshop = Event.Create(
                club.Id,
                "Workshop AI",
                DateTime.Today.AddDays(7).AddHours(14),
                capacity
            );

            Console.WriteLine($"Tên sự kiện: {workshop.Title}");
            Console.WriteLine($"Ngày tổ chức: {workshop.Date:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Sức chứa: {workshop.Capacity}");

            // 4. Đăng ký tham dự
            Console.WriteLine("\n4. Đăng ký tham dự");

            var registrations = new List<Registration>();

            foreach (var member in members.Take(2))
            {
                if (workshop.HasCapacity(registrations.Count))
                {
                    var registration = Registration.Create(member.Id, workshop.Id);
                    registrations.Add(registration);

                    Console.WriteLine($"{member.Name} đăng ký: {registration.Status}");
                }
                else
                {
                    Console.WriteLine("Sự kiện đã hết chỗ.");
                }
            }

            // 5. Xác nhận đăng ký
            Console.WriteLine("\n5. Xác nhận đăng ký");

            foreach (var registration in registrations)
            {
                registration.Confirm();
                Console.WriteLine($"Trạng thái: {registration.Status}");
            }

            // 6. Check-in
            Console.WriteLine("\n6. Check-in");

            var checkIns = new List<CheckIn>();

            if (registrations.Count > 0)
            {
                var checkIn = CheckIn.Create(registrations[0], "QrCode");
                checkIns.Add(checkIn);

                Console.WriteLine(checkIn.Summary());
            }

            // 7. Báo cáo tham dự
            Console.WriteLine("\n7. Báo cáo tham dự");
            Console.WriteLine($"Sự kiện: {workshop.Title}");
            Console.WriteLine($"Số đăng ký: {registrations.Count}");
            Console.WriteLine($"Số check-in: {checkIns.Count}");
            Console.WriteLine("Danh sách thành viên:");

            foreach (var registration in registrations)
            {
                var member = members.First(m => m.Id == registration.MemberId);
                bool attended = checkIns.Any(c => c.RegistrationId == registration.Id);

                Console.WriteLine($"- {member.Name}: {(attended ? "Đã check-in" : "Chưa check-in")}");
            }

            // 8. Kiểm tra quy tắc nghiệp vụ
            Console.WriteLine("\n8. Kiểm tra quy tắc nghiệp vụ");

            if (registrations.Count > 1)
            {
                var registration = registrations[1];
                registration.Cancel();

                Console.WriteLine($"Đã hủy đăng ký: {registration.Status}");

                try
                {
                    registration.Confirm();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Bắt lỗi nghiệp vụ: {ex.Message}");
                }
            }

            Console.WriteLine($"\n{separator}");
            Console.WriteLine("Demo complete!");
            Console.WriteLine(separator);

            Console.ReadKey();
        }
    }
}