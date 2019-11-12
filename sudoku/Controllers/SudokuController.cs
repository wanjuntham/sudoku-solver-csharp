using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sudoku;
using sudoku.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sudoku.Controllers
{
    [Route("api/[controller]")]
    public class SudokuController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public Board Post([FromBody]Board board)
        {
            Board newboard = new Board();
            var board_string = board.board;
            var convertedBoard = SudokuSolver.ConvertBoard(board_string);
            int N = convertedBoard.GetLength(0);

            if (SudokuSolver.solveSudoku(convertedBoard,N))
            {
                var boardString = SudokuSolver.DisplayBoard(convertedBoard, N);
                newboard.board = boardString;
                return newboard;
            }
            else
            {
                newboard.board = "No Solution";
                return newboard;
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
