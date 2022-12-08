pub fn main() {
    let data = include_str!("../input.txt")
        .lines()
        .map(|n| n.parse().expect("Couldn't read"))
        .collect::<Vec<i32>>();

    let p1 = part1(&data);

    println!("Num increases: {}", p1);

    let p2 = part2(&data);

    println!("Window increases: {}", p2);
}

fn part1(data: &Vec<i32>) -> i32 {
    let mut count = 0;
    for i in 1..data.len() {
        count += (data[i] > data[i - 1]) as i32;
    }
    count
}

fn part2(data: &Vec<i32>) -> i32 {
    let mut count = 0;
    for i in 1..data.len() - 2 {
        count += (data[i + 2] - data[i - 1] > 0) as i32;
    }
    count
}
