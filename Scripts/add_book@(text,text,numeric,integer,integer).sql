create function add_book(_name text, _author text, _price numeric, _type integer, _storage integer) returns integer
    language plpgsql
as
$$
BEGIN
    INSERT INTO common.books (name, author, price, type, present_on_storage) VALUES (_name, _author, _price, _type, _storage);
    RETURN currval('common.books_id_seq');
END
$$;

alter function add_book(text, text, numeric, integer, integer) owner to postgres;

