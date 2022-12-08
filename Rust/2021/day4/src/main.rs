use regex::Regex;

#[derive(Debug)]
struct Board {
    numbers: Vec<i32>,
    marked: Vec<bool>,
}

impl Board {
    fn new(repr: &str) -> Board {
        let re = Regex::new(r"\s+").unwrap();
        let line = re.replace_all(repr, " ");
        let split = line
            .trim()
            .split(" ")
            .into_iter()
            .map(|n| n.parse().unwrap())
            .collect();
        Board {
            numbers: split,
            marked: vec![false; 25],
        }
    }

    fn check_win(&self) -> bool {
        for i in 0..5 {
            // Check columns
            if self.marked.iter().skip(i).step_by(5).all(|x| *x == true) {
                return true;
            }
            // Check rows
            if self.marked.iter().skip(i * 5).take(5).all(|x| *x == true) {
                return true;
            }
        }

        false
    }

    fn winning_score(&self) -> i32 {
        self.numbers
            .iter()
            .enumerate()
            .filter(|(i, _)| self.marked[*i] == false)
            .map(|(_, n)| *n)
            .sum()
    }
}

fn main() {
    let (nums, grids) = include_str!("../input.txt")
        .split_once("\r\n\r\n")
        .expect("");

    let nums: Vec<i32> = nums
        .split(",")
        .into_iter()
        .map(|n| n.parse().unwrap())
        .collect();

    let mut boards: Vec<Board> = Vec::new();

    for grid in grids.split("\r\n\r\n").into_iter().collect::<Vec<&str>>() {
        boards.push(Board::new(grid));
    }

    println!("Part 1: {}", part1(&nums, &mut boards));
    println!("Part 2: {}", part2(&nums, &mut boards));
}

fn part1(nums: &Vec<i32>, boards: &mut Vec<Board>) -> i32 {
    for num in nums.iter() {
        for board in boards.iter_mut() {
            match board.numbers.iter().position(|n| n == num) {
                None => {}
                Some(n) => board.marked[n] = true,
            }
            if board.check_win() {
                return board.winning_score() * num;
            }
        }
    }
    0
}

fn part2(nums: &Vec<i32>, boards: &mut Vec<Board>) -> i32 {
    for num in nums.iter() {
        for board in boards.iter_mut() {
            match board.numbers.iter().position(|n| n == num) {
                None => {}
                Some(n) => board.marked[n] = true,
            }
        }
        if boards.len() == 1 {
            if boards[0].check_win() {
                return boards[0].winning_score() * num;
            }
        }
        boards.retain(|b| b.check_win() == false);
    }
    0
}
