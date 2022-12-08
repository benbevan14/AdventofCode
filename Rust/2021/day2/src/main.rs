fn main() {
    let mut data: Vec<(&str, i32)> = vec![];
    for line in include_str!("../input.txt").lines() {
        let split: Vec<&str> = line.split(' ').collect();
        data.push((split[0], split[1].parse().expect("Couldn't read number")));
    }

    println!("Part 1 answer: {}", part1(&data));
    println!("Part 2 answer: {}", part2(data));
}

fn part1(data: &Vec<(&str, i32)>) -> i32 {
    let mut pos = 0;
    let mut depth = 0;

    for (dir, num) in data {
        match dir {
            &"forward" => pos += num,
            &"down" => depth += num,
            &"up" => depth -= num,
            &_ => {}
        }
    }

    pos * depth
}

fn part2(data: Vec<(&str, i32)>) -> i32 {
    let mut pos = 0;
    let mut depth = 0;
    let mut aim = 0;

    for (dir, num) in data {
        match dir {
            "forward" => {
                pos += num;
                depth += aim * num;
            }
            "down" => aim += num,
            "up" => aim -= num,
            _ => {}
        }
    }

    pos * depth
}
