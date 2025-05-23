﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MahApps.Metro.Controls.Dialogs;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using WpfBookRentalShop01.Helpers;
using WpfBookRentalShop01.Models;

namespace WpfBookRentalShop01.ViewModels
{
    public partial class BooksViewModel : ObservableObject
    {
        private readonly IDialogCoordinator dialogCoordinator;

        private ObservableCollection<Book> _books;

        public ObservableCollection<Book> Books
        {
            get => _books;
            set => SetProperty(ref _books, value);
        }

        private Book _selectedBook;

        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                SetProperty(ref _selectedBook, value);
                _isUpdate = true;   // 수정 상태
            }
        }

        private bool _isUpdate;

        public BooksViewModel(IDialogCoordinator coordinator)
        {
            this.dialogCoordinator = coordinator;
            InitVariable();
            LoadGridFontDB();
        }


        private void InitVariable()
        {
            SelectedBook = new Book
            {
                Idx = 0,
                Author = string.Empty,
                Division = string.Empty,
                Names = string.Empty,
                DNames = string.Empty,
                Releasedate = DateTime.Now,
                ISBN = string.Empty,
                Price = 0,
            };
            _isUpdate = false;
        }

        [RelayCommand]
        public void SetInit()
        {
            InitVariable();
        }

        [RelayCommand]
        public async void SaveData()
        {
            try
            {
                string query = string.Empty;

                using (MySqlConnection conn = new MySqlConnection(Common.CONSTR))
                {
                    conn.Open();

                    if (_isUpdate)
                    {
                        query = @"UPDATE bookstbl
                                     SET author = @author,
                                         division = @division,
                                         names = @names,
                                         releasedate = @releasedate,
                                         isbn = @isbn,
                                         price = @price               
                                   WHERE idx = @idx";
                    }
                    else
                    {
                        query = @"INSERT INTO bookstbl (author, division, names, releasedate, isbn, price)
                                                 VALUES (@author, @divisions, @names, @releasedate, @isbn, @price);";
                    }

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@author", SelectedBook.Author);
                    cmd.Parameters.AddWithValue("@division", SelectedBook.Division);
                    cmd.Parameters.AddWithValue("@names", SelectedBook.Names);
                    cmd.Parameters.AddWithValue("@releasedate", SelectedBook.Releasedate);
                    cmd.Parameters.AddWithValue("@isbn", SelectedBook.ISBN);
                    cmd.Parameters.AddWithValue("@price", SelectedBook.Price);
                    // 업데이트 일때만 @idx가 필요함
                    if (_isUpdate) { cmd.Parameters.AddWithValue("@idx", SelectedBook.Idx); }

                    var resultCnt = cmd.ExecuteNonQuery();
                    if (resultCnt > 0)
                    {
                        Common.LOGGER.Info("책 데이터 저장 완료 !!");
                        await this.dialogCoordinator.ShowMessageAsync(this, "저장", "저장성공!!");
                    }
                    else
                    {
                        Common.LOGGER.Info("책 데이터 저장 실패 ..");
                        await this.dialogCoordinator.ShowMessageAsync(this, "저장", "저장실패..");
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LOGGER.Error(ex.Message);
                await this.dialogCoordinator.ShowMessageAsync(this, "오류", ex.Message);
            }

            LoadGridFontDB();
        }

        [RelayCommand]
        public async void DelData()
        {
            if (!_isUpdate)
            {
                await this.dialogCoordinator.ShowMessageAsync(this, "삭제", "데이터를 선택하세요");
                return;
            }

            var result = await this.dialogCoordinator.ShowMessageAsync(this, "삭제여부", "삭제하시겠습니까?", MessageDialogStyle.AffirmativeAndNegative);
            if (result == MessageDialogResult.Negative) return;     // Cancel했으면 메서드 빠져나감

            try
            {
                string query = "DELETE FROM bookstbl WHERE idx=@idx";

                using (MySqlConnection conn = new MySqlConnection(Common.CONSTR))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idx", SelectedBook.Idx);

                    int resultCnt = cmd.ExecuteNonQuery();

                    if (resultCnt > 0)
                    {
                        Common.LOGGER.Info($"책 데이터 {SelectedBook.Idx} / {SelectedBook.Names} 삭제완료");
                        await this.dialogCoordinator.ShowMessageAsync(this, "삭제", "삭제 성공 !! ");
                    }
                    else
                    {
                        Common.LOGGER.Warn("책 데이터 삭제 실패..");
                        await this.dialogCoordinator.ShowMessageAsync(this, "삭제", "삭제 실패..");
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LOGGER.Error(ex.Message);
                await this.dialogCoordinator.ShowMessageAsync(this, "오류", ex.Message);
            }

            LoadGridFontDB();
        }

        private string _message;

        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

   
        private async void LoadGridFontDB()
        {
            try
            {
                //string query = "SELECT idx, author, division, names, releasedate, isbn, price FROM bookstbl";
                string query = @"SELECT b.Idx, b.Author, b.Division, b.Names, b.ReleaseDate, b.ISBN, b.Price,
                                        d.Names AS dNames
                                   FROM bookstbl AS b, divtbl AS d
                                  WHERE b.Division = d.Division
                                  ORDER by b.Idx";
                ObservableCollection<Book> books = new ObservableCollection<Book>();

                using (MySqlConnection conn = new MySqlConnection(Common.CONSTR))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var idx = reader.GetInt32("idx");
                        var author = reader.GetString("author");
                        var division = reader.GetString("division");
                        var names = reader.GetString("names");
                        var dnames = reader.GetString("dnames");
                        var releasedate = reader.GetDateTime("releasedate");
                        var isbn = reader.GetString("isbn");
                        var price = reader.GetInt32("price");

                        books.Add(new Book
                        {
                            Idx = idx,
                            Author = author,
                            Division = division,
                            Names = names,
                            DNames = dnames,
                            Releasedate = releasedate,
                            ISBN = isbn,
                            Price = price
                        });
                    }
                }

                Books = books;  // View에 바인딩 필수!
            }
            catch (Exception ex)
            {
                Common.LOGGER.Error(ex.Message);
                //MessageBox.Show(ex.Message);
                await this.dialogCoordinator.ShowMessageAsync(this, "오류", ex.Message);
            }

            Common.LOGGER.Info("책 데이터 로드");
        }
    }
}
