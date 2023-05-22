import "./Icon.css";

export function Icon({ i }: IProps) {
    return <div className="Icon">{i}</div>;
}

interface IProps {
    i: Icons | string;
}

export type Icons =
    | "search"
    | "favorite"
    | "settings"
    | "delete"
    | "edit"
    | "done"
    | "menu"
    | "close"
    | "add"
    | "star"
    | "refresh"
    | "Bolt"
    | "block"
    | "Menu_Book"
    | "Home"
    | "arrow_back"
    | "arrow_forward"
    | "arrow_downward"
    | "arrow_upward"
    | "fork_left"
    | "fork_left"
    | "swap_vert"
    | "counter_1"
    | "counter_2"
    | "counter_3"
    | "counter_4"
    | "counter_5"
    | "counter_6"
    | "counter_7"
    | "counter_8"
    | "counter_9"
    | "flight"
    | "flight_takeoff"
    | "flight_land";
