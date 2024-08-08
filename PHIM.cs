using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nhom6_DoAn_
{
    public partial class PHIM
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHIM()
        {
            this.SUATCHIEUx = new HashSet<SUATCHIEU>();
        }

        [Display(Name = "Mã Phim")]
        [Required(ErrorMessage = "Mã Phim không được để trống.")]
        public string MAPHIM { get; set; }

        [Display(Name = "Tên Phim")]
        [Required(ErrorMessage = "Tên Phim không được để trống.")]
        public string TENPHIM { get; set; }

        [Display(Name = "Ảnh Bìa")]
        [Required(ErrorMessage = "Ảnh Bìa không được để trống.")]
        public string ANHBIA { get; set; }

        [Display(Name = "Trailer")]
        [Required(ErrorMessage = "Trailer không được để trống.")]
        public string TRAILER { get; set; }

        [Display(Name = "Đạo Diễn")]
        [Required(ErrorMessage = "Đạo Diễn không được để trống.")]
        public string DAODIEN { get; set; }

        [Display(Name = "Diễn Viên")]
        [Required(ErrorMessage = "Diễn Viên không được để trống.")]
        public string DIENVIEN { get; set; }

        [Display(Name = "Thể Loại")]
        [Required(ErrorMessage = "Thể Loại không được để trống.")]
        public string THELOAI { get; set; }

        [Display(Name = "Thời Lượng")]
        [Required(ErrorMessage = "Thời Lượng không được để trống.")]
        public Nullable<int> THOILUONG { get; set; }

        [Display(Name = "Ngôn Ngữ")]
        [Required(ErrorMessage = "Ngôn Ngữ không được để trống.")]
        public string NGONNGU { get; set; }

        [Display(Name = "Khởi Chiếu")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Khởi Chiếu không được để trống.")]
        public Nullable<System.DateTime> KHOICHIEU { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUATCHIEU> SUATCHIEUx { get; set; }

        [NotMapped]
        public HttpPostedFileBase PosterFile { get; set; }
    }
}
