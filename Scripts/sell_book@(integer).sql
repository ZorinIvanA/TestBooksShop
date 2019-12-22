create function sell_book(_id integer) returns void
    language plpgsql
as
$$
BEGIN
    IF NOT EXISTS(SELECT id FROM common.books WHERE id = _id) THEN
        RAISE EXCEPTION 'ID книги не существует: %', _id ;
    END IF;

    UPDATE common.books SET present_on_storage=present_on_storage - 1 WHERE id = _id;
    INSERT INTO common.sales (book_id, sale_date) VALUES (_id, now());

END;
$$;

alter function sell_book(integer) owner to postgres;

