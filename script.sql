USE QuanCafe
GO
--///////////////////////////////////Procedure and Function/////////////////////////////
-- Thêm Loại sp
create proc [dbo].[insertLOAISP] (@ten nvarchar(50), @anh nvarchar(200), @mota ntext, @ngaytao date, @idhang int, @trangthai nvarchar(20))
as
begin
	insert into LOAISP
	values (@ten, @anh, @mota, @ngaytao, @idhang, @trangthai)
end
GO
--Update Loại sản phẩm
create proc [dbo].[updateLOAISP] (@id int, @ten nvarchar(50), @anh nvarchar(200), @mota ntext, @ngaytao date, @idhang int, @trangthai nvarchar(20))
as
begin
	update LOAISP
	set TEN = @ten, ANH = @anh, MOTA = @mota, NGAYTAO = @ngaytao, HANGSX_ID = @idhang, TRANGTHAI = @trangthai
	where ID = @id
end
GO
--Xóa Loại sản phẩm
create proc deleteLOAISP (@id int)
as
begin
	update LOAISP
	set TRANGTHAI = N'Ngừng bán' 
	where ID=@id
	update SANPHAM
	set TRANGTHAI = N'Ngừng bán'
	where LOAISP_ID = @id
end
GO
-- Lấy loại sản phẩm theo id
create function getLOAISPbyID(@id int) 
returns table
	return select * from LOAISP where ID = @id

--Tạo sản phẩm mới
go
create proc insertSANPHAM (@khoiluong int, @anh nvarchar(200), @mota ntext, @soluong int, @idloaisp int)
as
begin
	insert into SANPHAM values(@khoiluong, @anh, @mota, @soluong,@idloaisp,N'Đang bán')
end
GO
--Update thông tin sản phẩm
create proc updateSANPHAM (@id int, @khoiluong int, @anh nvarchar(200), @mota ntext, @soluong int, @idloaisp int)
as
begin
	update SANPHAM 
	set KHOILUONG = @khoiluong, ANH = @anh, MOTA = @mota, SOLUONG = @soluong, LOAISP_ID = @idloaisp
	where ID = @id
end
GO
--Xóa sản phẩm
create proc deleteSANPHAM (@id int)
as
begin
	update SANPHAM
	set TRANGTHAI = N'Ngừng bán'
	where ID = @id
end
GO
--lấy sản phẩm theo idsanpham
create function getSanPhamById(@id int) returns table
	return (select * from SANPHAM where ID = @id)
GO
--lấy các sản phẩm theo ID Loại sản phẩm
create function getSanPhamByIdLSP(@idLSP int) returns table
	return (select ID,KHOILUONG,ANH,MOTA,SOLUONG,LOAISP_ID from SANPHAM  where LOAISP_ID = @idLSP)
GO
--lấy giá bán của sản phẩm hiện tại
create function getPriceBySanPham(@id int) returns table
	return select p.ID, p.GIABAN, p.BATDAU, p.KETTHUC, p.SANPHAM_ID from Price p join SANPHAM s on p.SANPHAM_ID = s.ID 
		where s.ID = @id and (p.BATDAU <= GETDATE() and p.KETTHUC >= GETDATE() or p.BATDAU <= GETDATE() and p.KETTHUC is null)
GO
--Thêm giá  cho sản phẩm mới
Create proc InsertPrice(@idSP int, @giaban int)
as
begin
	insert into PRICE values(@giaban, GETDATE(), NULL, @idSP)
end
GO
--Update giá sản phẩm
Create proc UpdatePrice(@idSP int, @giaban int)
as
begin
	declare @giacu int, @idPrice int
	select @giacu = GIABAN, @idPrice = ID from dbo.getPriceBySanPham(@idSP)
	if (@giaban != @giacu)
	begin
		update PRICE
		set KETTHUC = GETDATE() - 1
		where ID = @idPrice
		exec InsertPrice @idSP,@giaban
	end
end
GO
---Nhập hàng----
create proc insertNhaphang (@idNCC int)
as
begin
	insert into NHAPHANG values(GETDATE(), @idNCC )
end
GO
--Lấy ID Nhập hàng vừa mới thêm
create function getNewIdNhapHang () returns int
as
begin
	declare @idNhapHang int
	select top 1 @idNhapHang = ID from NHAPHANG order by ID desc 
	return @idNhapHang
end
GO
---Nhập chi tiết nhập hàng--
create proc insertCHITIETNHAPHANG (@idSanPham int, @idNhapHang int, @soluongnhap int, @gianhap int)
as
begin
	insert into CHITIETNHAPHANG
	values (@idSanPham, @idNhapHang, @soluongnhap, @gianhap, @soluongnhap)
end
GO
---Thêm nhà cung cấp---
create proc insertNCC (@ten nvarchar(50), @diachi nvarchar(100), @sdt varchar(15))
as
begin
	insert into NHACUNGCAP values (@ten, @diachi, @sdt)
end
GO
--Thêm hãng sản xuất--
create proc insertHSX(@ten nvarchar(50))
as
begin
	insert into HANGSX values (@ten)
end
GO
---Thêm mới đơn hàng
Create proc insertDonHang(@nguoinhan nvarchar(50), @sdt varchar(15), @diachi nvarchar(100), @idKH int)
as
begin
	insert into DONHANG
		values (GETDATE(), @nguoinhan, @sdt, @diachi, 0, 1, @idKH)
end
GO
--Lấy Đơn hàng theo ID
create function getDonHang(@id int)
	returns table return select * from DONHANG where ID = @id
GO
--Lấy ID đơn hàng vừa thêm
create function getNewIdDonHang() returns int
as
begin
	declare @id int
	select top 1 @id = ID from DONHANG order by ID desc
	return @id
end
GO
select dbo.getNewIdDonHang()
--Thêm các Chi tiết đơn hàng
create proc insertCTDH(@idSP int, @idDH int, @soluong int, @giaban int)
as
begin
	insert into CHITIETDONHANG
		values (@idSP, @idDH, @soluong, @giaban)
end
GO
--Xuất hàng
create proc insertXuatHang(@idCTNhapHang int, @idCTDonHang int, @soluongxuat int, @gianhap int, @giaban int)
as
begin
	insert into XUATHANG 
		values (@idCTDonHang, @idCTNhapHang, @soluongxuat, @gianhap, @giaban)
end
GO
--Update trạng thái đơn hàng
Create proc updateTrangThaiDH(@idDH int, @idTrangThaiMoi int)
as
begin
	declare @idCTDH int
	if (@idTrangThaiMoi = 3)
		begin
			declare @idSP int, @ngaydat date
			select @ngaydat = NGAYDAT from DONHANG where ID = @idDH
			declare cursorCTDH cursor for
				select ID, SANPHAM_ID from CHITIETDONHANG where DONHANG_ID = @idDH
			open cursorCTDH
			FETCH NEXT FROM cursorCTDH into @idCTDH, @idSP
			WHILE @@FETCH_STATUS = 0
			BEGIN
				declare @sl int, @gianhap int, @giaban int, @doanhthu int, @loinhuan int
				declare cursorXH cursor for select SOLUONGXUAT, GIABAN, GIANHAP from XUATHANG where CHITIETDH_ID = @idCTDH
				open cursorXH
				FETCH NEXT FROM cursorXH into @sl, @giaban, @gianhap
				WHILE @@FETCH_STATUS = 0
				BEGIN
					declare @count int
					select @count = count(ID) from THONGKE where NGAY = @ngaydat and SANPHAM_ID = @idSP
					if (@count = 0)
						begin
							set @doanhthu = @sl*@giaban
							set @loinhuan = @sl*(@giaban-@gianhap)
							insert into THONGKE values (@ngaydat, @idSP, @sl, @doanhthu, @loinhuan)
						end
					else
						begin
							select @doanhthu = DOANHTHU, @loinhuan = LOINHUAN from THONGKE
									where NGAY = @ngaydat and SANPHAM_ID = @idSP
							set @doanhthu = @doanhthu + @sl*@giaban
							set @loinhuan = @loinhuan + @sl*(@giaban-@gianhap)
							update THONGKE
							set SOLUONGBAN = SOLUONGBAN + @sl, DOANHTHU = @doanhthu, LOINHUAN = @loinhuan
							where NGAY = @ngaydat and SANPHAM_ID = @idSP
						end
					FETCH NEXT FROM cursorXH into @sl, @giaban, @gianhap
				END
				close cursorXH
				deallocate cursorXH
				FETCH NEXT FROM cursorCTDH into @idCTDH, @idSP
			END
			close cursorCTDH
			deallocate cursorCTDH
			update DONHANG
			set TRANGTHAI_ID = @idTrangThaiMoi
			where ID = @idDH
		end
	else if (@idTrangThaiMoi = 4)
		begin
			declare cursorCTDH cursor for
				select ID from CHITIETDONHANG where DONHANG_ID = @idDH
			open cursorCTDH
			FETCH NEXT FROM cursorCTDH into @idCTDH
			declare @idXH int
			WHILE @@FETCH_STATUS = 0
			begin
				select @idXH = ID from XUATHANG
					where CHITIETDH_ID = @idCTDH
				delete XUATHANG where ID = @idXH
				FETCH NEXT FROM cursorCTDH into @idCTDH
			end
			close cursorCTDH
			deallocate cursorCTDH
			update DONHANG
			set TRANGTHAI_ID = @idTrangThaiMoi
			where ID = @idDH
		end
	else
		begin
			update DONHANG
			set TRANGTHAI_ID = @idTrangThaiMoi
			where ID = @idDH
		end
end
GO
--////////////////////////////////////////////TRIGGER//////////////////////////////
--Update lại số lượng sản phẩm khi nhập lô mới
create trigger updateSoLuongSP on CHITIETNHAPHANG for insert
as
begin
	declare @idSanPham int, @soluongnhap int
	select @idSanPham = SANPHAM_ID, @soluongnhap = SOLUONGNHAP from inserted
	update SANPHAM
	set SOLUONG = SOLUONG + @soluongnhap
	where ID = @idSanPham
end
GO
--Update lại số lượng còn lại trong lô và số lượng sản phẩm khi xuất hàng
create trigger Insert_updateSoLuong on XUATHANG for insert
as
begin
	declare @idCTNhapHang int, @soluongxuat int
	select @idCTNhapHang = NHAPHANG_ID, @soluongxuat = SOLUONGXUAT from inserted

	update CHITIETNHAPHANG
	set SOLUONGCONLAI = SOLUONGCONLAI - @soluongxuat
	where ID = @idCTNhapHang

	declare @idSP int
	select @idSP = SANPHAM_ID from CHITIETNHAPHANG where ID = @idCTNhapHang
	update SANPHAM
	set SOLUONG = SOLUONG - @soluongxuat
	where ID = @idSP
end
GO
--Update lại số lượng còn lại trong lô và số lượng sản phẩm khi xuất hàng
create trigger Delete_updateSoLuong on XUATHANG for delete
as
begin
	declare @idCTNhapHang int, @soluongxuat int
	select @idCTNhapHang = NHAPHANG_ID, @soluongxuat = SOLUONGXUAT from inserted

	update CHITIETNHAPHANG
	set SOLUONGCONLAI = SOLUONGCONLAI + @soluongxuat
	where ID = @idCTNhapHang

	declare @idSP int
	select @idSP = SANPHAM_ID from CHITIETNHAPHANG where ID = @idCTNhapHang
	update SANPHAM
	set SOLUONG = SOLUONG + @soluongxuat
	where ID = @idSP
end
GO
--Update lại tổng tiền trong đơn hàng khi thêm chi tiết đơn hàng
Create Trigger Insert_UpdateTongTien on CHITIETDONHANG for insert
as
begin
	declare @idDH int, @sl int, @giaban int
	select @idDH = DONHANG_ID, @sl = SOLUONG, @giaban = DONGIA from inserted
	update DONHANG
	set TONGTIEN = TONGTIEN + @sl*@giaban
	where ID = @idDH
end
GO